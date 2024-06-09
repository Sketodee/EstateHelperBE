using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Application.Contract.Dtos.ConsultantGroups
{
    public class EditConsultantGroupDto
    {
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; }  
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [EmailAddress, Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Account Details is required")]
        public string AccountDetails { get; set; } = string.Empty;
        [Required(ErrorMessage = "Bank Provider is required")]
        public string BankProvider { get; set; } = string.Empty;
        [Required(ErrorMessage = "Account Name is required")]
        public string AccountName { get; set; } = string.Empty;
        public string Description { get; set; }
        public string AccountManagerId { get; set; }
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
