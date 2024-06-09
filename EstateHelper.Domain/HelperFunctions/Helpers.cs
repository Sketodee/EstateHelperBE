using EstateHelper.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Domain.HelperFunctions
{
    public class Helpers
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public Helpers(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<AppUser?> ReturnLoggedInUser()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var getUser = httpContext?.User.Identity?.Name;

            var findUser = await _userManager.FindByNameAsync(getUser);

            return findUser;
        }

        public string GenerateAlphanumericID(int length)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] bytes = new byte[length * 2]; // Each character takes 2 bytes

                rng.GetBytes(bytes);

                StringBuilder idBuilder = new StringBuilder();

                for (int i = 0; i < length; i++)
                {
                    ushort value = BitConverter.ToUInt16(bytes, i * 2);
                    char c = Convert.ToChar((value % 26) + 65);
                    idBuilder.Append(c);
                }

                string id = idBuilder.ToString();

                return id;
            }
        }
    }
}
