using AutoMapper;
using Azure;
using Azure.Core;
using EstateHelper.Application.Contract.Dtos.Login;
using EstateHelper.Application.Contract.Dtos.User;
using EstateHelper.Domain.HelperFunctions;
using EstateHelper.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Domain.User
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _appUserManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Helpers _helpers;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManager(IUserRepository userRepository, IConfiguration configuration, UserManager<AppUser> appUserManager, IHttpContextAccessor httpContextAccessor, 
            Helpers helpers, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _appUserManager = appUserManager;
            _httpContextAccessor = httpContextAccessor;
            _helpers = helpers;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<LoginResponseDto> Login (LoginRequestDto request)
        {
            AppUser appUser = await _appUserManager.FindByEmailAsync(request.Email) ?? throw new Exception("Invalid Credential");
            var passwordCheck = await _appUserManager.CheckPasswordAsync(appUser, request.Password) ? true : throw new Exception("Invalid Credential");
            var userRoles = await _appUserManager.GetRolesAsync(appUser);


            var token =await GetToken(appUser);
            var returnedToken = new JwtSecurityTokenHandler().WriteToken(token);

            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken, appUser);

            appUser.RefreshToken = refreshToken.Token;
            appUser.TokenCreated = refreshToken.Created;
            appUser.TokenExpires = refreshToken.Expires;

            await _appUserManager.UpdateAsync(appUser);

            var userDetails = new LoginResponseDto
            {
                //Role = userRoles[0],
                //Email = appUser.Email,
                User = $"{appUser.Surname} {appUser.FirstName}",
                Token = returnedToken,
                //RefreshToken = refreshToken.Token
            };

            return userDetails; 
           
        }

        public async Task<AppUser> SignUpGeneralAdmin(CreateUserDto request)
        {
            //initialize new referrer generation list 
            List<int> refGen = new();
            request.ReferrerId ??= 600;
            refGen.Add(600);
            var appUser = _mapper.Map<AppUser>(request);
            appUser.RefererGeneration = refGen;

            //check to allow only one general admin
            if (await _roleManager.RoleExistsAsync(EstateHelperEnums.EstateHelperRoles.GeneralAdmin.ToString()))
            {
                //await _appUserManager.AddToRoleAsync(appUser, EstateHelperEnums.EstateHelperRoles.GeneralAdmin.ToString());
                throw new Exception("Can only add one General Admin");
            }

            IdentityResult result = await _appUserManager.CreateAsync(appUser, request.Password) ?? throw new Exception("Error Creating User");
            if (!result.Succeeded) { throw new Exception($"Can't create user - {result.Errors.FirstOrDefault()?.Description}"); };


            //check if user role already exists
            if (!await _roleManager.RoleExistsAsync(EstateHelperEnums.EstateHelperRoles.GeneralAdmin.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole(EstateHelperEnums.EstateHelperRoles.GeneralAdmin.ToString()));
                await _roleManager.CreateAsync(new IdentityRole(EstateHelperEnums.EstateHelperRoles.Admin.ToString()));
                await _roleManager.CreateAsync(new IdentityRole(EstateHelperEnums.EstateHelperRoles.User.ToString()));

                //add general admin to role 
                await _appUserManager.AddToRoleAsync(appUser, EstateHelperEnums.EstateHelperRoles.GeneralAdmin.ToString());
            }
               

            return appUser;
        }

        public async Task<AppUser> SignUpUser(CreateUserDto request)
        {
            request.ReferrerId ??= 600;
            var appUser = _mapper.Map<AppUser>(request);

            //find if referrer is in the system
            var referrer = await _userRepository.SingleOrDefaultAsync(x => x.Link == appUser.ReferrerId) ?? throw new Exception("Referrer not found");
            var refGen = referrer.RefererGeneration;
            if (!refGen.Any(n => n == referrer.Link)) refGen.Add(referrer.Link);

            //add referrerId to referrer generation
            appUser.RefererGeneration = refGen;

            IdentityResult result = await _appUserManager.CreateAsync(appUser, request.Password) ?? throw new Exception("Error Creating User");
            if (!result.Succeeded) { throw new Exception($"Can't create user - {result.Errors.FirstOrDefault()?.Description}"); };

            //check if user role already exists
            if (!await _roleManager.RoleExistsAsync(EstateHelperEnums.EstateHelperRoles.User.ToString()))
                //await _roleManager.CreateAsync(new IdentityRole("User"));
                throw new Exception("Can't add user because role doesn't exist");

            if (await _roleManager.RoleExistsAsync(EstateHelperEnums.EstateHelperRoles.User.ToString()))
            {
                await _appUserManager.AddToRoleAsync(appUser, EstateHelperEnums.EstateHelperRoles.User.ToString());
            }

            return appUser;
        }

        private async Task<JwtSecurityToken> GetToken(AppUser appUser)
        {
            var userRoles = await _appUserManager.GetRolesAsync(appUser);

            var authClaims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, appUser.UserName),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(30),
                //expires: DateTime.Now.AddSeconds(30),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7)
                //Expires = DateTime.Now.AddMinutes(1)
            };

            return refreshToken;
        }

        private async void SetRefreshToken(RefreshToken newRefreshToken, AppUser appUser)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, 
                SameSite = SameSiteMode.None,
                Expires = newRefreshToken.Expires
            };
            var httpContext = _httpContextAccessor.HttpContext;
            httpContext.Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
        }

        public async Task<GetLoggedInUserDto> GetLoggedInUser()
        {
            var result = await _helpers.ReturnLoggedInUser();
            return new GetLoggedInUserDto
            {
                FullName = result?.Surname + " "+  result?.FirstName
            };
        }

        public async Task<string> GetRefreshToken()
        {
            //var user = await _helpers.ReturnLoggedInUser();
            var httpContext = _httpContextAccessor.HttpContext;
            var refreshToken = httpContext.Request.Cookies.TryGetValue("refreshToken", out string value) ? value : null;
            //check the user that owns the refresh token being sent 
            var user = await _userRepository.SingleOrDefaultAsync(x => x.RefreshToken == refreshToken) ?? throw new Exception("Invalid Refresh Token"); 
            //_ = user.RefreshToken.Equals(refreshToken) ? true : throw new Exception("Invalid Refresh Token");
            _ = user.TokenExpires < DateTime.Now ? throw new Exception("Token Expired") : false;

            var token = await GetToken(user);
            var returnedToken = new JwtSecurityTokenHandler().WriteToken(token);

            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken, user);

            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;

            await _appUserManager.UpdateAsync(user);

            return returnedToken;
        }

        public async Task<string> Logout()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var token = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                return "Invalid token";
            }
        
            // Clear the authToken cookie
            httpContext.Response.Cookies.Delete("refreshToken");

            // Optionally, you could log the user out from the authentication scheme
            // await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return "Logout sucessful";
        }

        public async Task<bool> AddUserToRole(string roleName, string userId)
        {
            //find if user exists 
            var user = await _userRepository.SingleOrDefaultAsync(x => x.Id == userId) ?? throw new Exception("User not found");
            //find if role exist 
            if (!await _roleManager.RoleExistsAsync(roleName)) throw new Exception("Role doesn't exist");
            if (await _appUserManager.IsInRoleAsync(user, roleName)) throw new Exception("User already exist in role");
            var result = await _appUserManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded) throw new Exception("Can't add user to role"); 
            return result.Succeeded;    

        }
    }
}
