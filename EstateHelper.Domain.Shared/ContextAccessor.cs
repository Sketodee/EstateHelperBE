﻿using EstateHelper.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Domain.Shared
{
    public class ContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ContextAccessor(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager )
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<AppUser?> ReturnUser()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var getUser = httpContext?.User.Identity?.Name;

            var findUser = await _userManager.FindByNameAsync(getUser);

            return findUser; 
        }

    }
}
