using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Domain.Models
{
    public class AppUser: IdentityUser
    {
        public string Surname { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string AccountDetails { get; set; } = string.Empty;
        public string BankProvider { get; set; } = string.Empty;
        public string AccountName { get; set; } = string.Empty;
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Link { get; set; }
        public int ReferrerId { get; set; }
        public List<int> RefererGeneration { get; set; }    
        public string PhoneNumber { get; set; }
        public bool isActive { get; set; } = true;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int LoginCount { get; set; } = 0;
        public string RefreshToken { get; set; } = string.Empty; 
        public DateTime TokenCreated { get; set; } 
        public DateTime TokenExpires { get; set; }

    }

}
