using AutoMapper;
using Azure;
using EstateHelper.Application.Contract.Dtos.User;
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

        public async Task<AppUser> SignUpAdmin(CreateUserDto request)
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
            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new IdentityRole("Admin"));

            if (await _roleManager.RoleExistsAsync("Admin"))
            {
                await _userManager.AddToRoleAsync(appUser, "Admin");
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
            if (!await _roleManager.RoleExistsAsync("User"))
                await _roleManager.CreateAsync(new IdentityRole("User"));

            if (await _roleManager.RoleExistsAsync("User"))
            {
                await _userManager.AddToRoleAsync(appUser, "User");
            }

            return appUser; 

        }
    }
}
