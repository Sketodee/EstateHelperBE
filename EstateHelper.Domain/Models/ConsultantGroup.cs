using EstateHelper.Application.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Domain.Models
{
    public class ConsultantGroup : BaseAuditedEntity
    {
        public string Id { get; set; }  = Guid.NewGuid().ToString();    
        public string Name { get; set; }
        public string AccountDetails { get; set; } = string.Empty;
        public string BankProvider { get; set; } = string.Empty;
        public string AccountName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; }
        public int ReferrerId { get; set; }
        public string Description { get; set; } 
        public string Code { get; set; }    
        public List<string> MembersId { get; set; } 
        public string AccountManagerId { get; set; }
    }
}
