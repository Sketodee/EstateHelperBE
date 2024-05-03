using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Application.Contract.Dtos.User
{
    public class CreateUserDto
    {
        public string Surname { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string AccountDetails { get; set; } = string.Empty;

        public string BankProvider { get; set; } = string.Empty;

        public string AccountName { get; set; } = string.Empty;

        public int? ReferrerId { get; set; }

        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
