using AutoMapper;
using Azure;
using EstateHelper.Application.Contract.Dtos.User;
using EstateHelper.Domain.HelperFunctions;
using EstateHelper.Domain.Models;
using EstateHelper.Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.EntityFramework.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserRepository(UserManager<AppUser> userManager, AppDbContext context, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager,
             IMapper mapper, IConfiguration configuration)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<AppUser> FindByIdAsync(string id)
        {
            var user = await _context.Users.Where(x => x.Id == id && x.isActive).FirstOrDefaultAsync() ?? throw new Exception($"No user with Id {id}");
            return user; 
        }

        public async Task<List<AppUser>> GetAllAsync()
        {
            var allUsers = await _context.Users.ToListAsync();
            return allUsers; 
        }

        public async Task<AppUser> SignUpGeneralAdmin(CreateUserDto request)
        {
            //initialize new referrer generation list 
            List<int> refGen = new();
            request.ReferrerId ??= 600;
            refGen.Add(600);
            var appUser = _mapper.Map<AppUser>(request);
            appUser.RefererGeneration = refGen;

            IdentityResult result = await _userManager.CreateAsync(appUser, request.Password) ?? throw new Exception("Error Creating User");
            if (!result.Succeeded) { throw new Exception($"Can't create user - {result.Errors.FirstOrDefault()?.Description}"); };

            //check if user role already exists
            if (!await _roleManager.RoleExistsAsync(EstateHelperEnums.EstateHelperRoles.GeneralAdmin.ToString()))
                await _roleManager.CreateAsync(new IdentityRole(EstateHelperEnums.EstateHelperRoles.GeneralAdmin.ToString()));
                await _roleManager.CreateAsync(new IdentityRole(EstateHelperEnums.EstateHelperRoles.Admin.ToString()));
                await _roleManager.CreateAsync(new IdentityRole(EstateHelperEnums.EstateHelperRoles.User.ToString()));

            if (await _roleManager.RoleExistsAsync(EstateHelperEnums.EstateHelperRoles.GeneralAdmin.ToString()))
            {
                await _userManager.AddToRoleAsync(appUser, EstateHelperEnums.EstateHelperRoles.GeneralAdmin.ToString());
            }

            return appUser;
        }

        public async Task<AppUser> SignUpUser(CreateUserDto request)
        {
            request.ReferrerId ??= 600;
            var appUser = _mapper.Map<AppUser>(request);

            //find if referrer is in the system
            var referrer = await _context.Users.Where(x => x.Link == appUser.ReferrerId).FirstOrDefaultAsync() ?? throw new Exception("Referrer not found");
            var refGen = referrer.RefererGeneration;
            if (!refGen.Any(n => n == referrer.Link)) refGen.Add(referrer.Link);

            //add referrerId to referrer generation
            appUser.RefererGeneration = refGen;

            IdentityResult result = await _userManager.CreateAsync(appUser, request.Password) ?? throw new Exception("Error Creating User");
            if(!result.Succeeded) { throw new Exception($"Can't create user - {result.Errors.FirstOrDefault()?.Description}"); };

            //check if user role already exists
            if (!await _roleManager.RoleExistsAsync(EstateHelperEnums.EstateHelperRoles.User.ToString()))
                //await _roleManager.CreateAsync(new IdentityRole("User"));
                throw new Exception("Can't add user because role doesn't exist"); 

            if (await _roleManager.RoleExistsAsync(EstateHelperEnums.EstateHelperRoles.User.ToString()))
            {
                await _userManager.AddToRoleAsync(appUser, EstateHelperEnums.EstateHelperRoles.User.ToString());
            }

            return appUser; 

        }

        public Task<AppUser> UpdateAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}
