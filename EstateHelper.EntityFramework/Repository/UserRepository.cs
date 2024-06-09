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
using System.Linq.Expressions;
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

        public async Task<AppUser> SingleOrDefaultAsync(Expression<Func<AppUser, bool>> predicate)
        {
            var user = await _context.Set<AppUser>().FirstOrDefaultAsync(predicate); //?? throw new Exception("User not found"); 
            return user; 
        }

        public Task<AppUser> UpdateAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}
