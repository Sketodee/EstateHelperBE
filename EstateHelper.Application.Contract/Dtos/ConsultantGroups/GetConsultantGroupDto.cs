using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Application.Contract.Dtos.ConsultantGroups
{
    public class GetConsultantGroupDto : BaseAuditedEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }   
        public string AccountDetails { get; set; }
        public string BankProvider { get; set; } 
        public string AccountName { get; set; }
        public int ReferrerId { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public List<string> MembersId { get; set; }
        public string AccountManagerId { get; set; }
        public string PhoneNumber { get; set; } 
    }
}
