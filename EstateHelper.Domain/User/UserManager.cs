using Azure;
using EstateHelper.Application.Contract.Dtos.Login;
using EstateHelper.Application.Contract.Dtos.User;
using EstateHelper.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

        public UserManager(IUserRepository userRepository, IConfiguration configuration, UserManager<AppUser> appUserManager, IHttpContextAccessor httpContextAccessor )
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _appUserManager = appUserManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginResponseDto> Login (LoginRequestDto request)
        {
            AppUser appUser = await _appUserManager.FindByEmailAsync(request.Email) ?? throw new Exception("Invalid Credential");
            var passwordCheck = await _appUserManager.CheckPasswordAsync(appUser, request.Password) ? true : throw new Exception("Invalid Credential");

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

            var token = GetToken(authClaims);

            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken, appUser);

            appUser.RefreshToken = refreshToken.Token;
            appUser.TokenCreated = refreshToken.Created;
            appUser.TokenExpires = refreshToken.Expires;

            await _appUserManager.UpdateAsync(appUser);

            var returnedToken = new JwtSecurityTokenHandler().WriteToken(token);

            var userDetails = new LoginResponseDto
            {
                Role = userRoles[0],
                Email = appUser.Email,
                Id = appUser.Id,
                Token = returnedToken,
            };

            return userDetails; 
           
        }

        public async Task<AppUser> SignUpAdmin(CreateUserDto request)
        {
            var result = await _userRepository.SignUpAdmin(request);
            return result;
        }

        public async Task<AppUser> SignUpUser(CreateUserDto request)
        {
            var result = await _userRepository.SignUpUser(request);
            return result; 
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
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
            };

            return refreshToken;
        }

        private async void SetRefreshToken(RefreshToken newRefreshToken, AppUser appUser)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            var httpContext = _httpContextAccessor.HttpContext;
            httpContext.Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

           
        }
    }
}
