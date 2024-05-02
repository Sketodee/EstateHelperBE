using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Domain.Models
{
    public class AppUser: IdentityUser
    {
        public bool isActive { get; set; } = true;
        public string CreatedOn { get; set; } = DateTime.Now.ToString("dd/MMM/yyyy");
        public string? Comment { get; set; }
        public int LoginCount { get; set; } = 0;
        public ICollection<IdentityRole> Roles { get; set; }
    }
}
