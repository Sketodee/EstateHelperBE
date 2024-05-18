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
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string AccountDetails { get; set; } = string.Empty;
        public string BankProvider { get; set; } = string.Empty;
        public string AccountName { get; set; } = string.Empty;
        public int ReferrerId { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public List<int> MembersId { get; set; }
        public int AccountManagerId { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
