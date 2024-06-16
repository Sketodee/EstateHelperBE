using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Application.Contract.Dtos.User
{
    public class GetUserDto
    {
        public string Id { get; set; }  
        public string Surname { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string AccountDetails { get; set; } = string.Empty;

        public string BankProvider { get; set; } = string.Empty;

        public string AccountName { get; set; } = string.Empty;

        public int? ReferrerId { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool isActive { get; set; } = true;

        public string PhoneNumber { get; set; } = string.Empty;
    }
}
