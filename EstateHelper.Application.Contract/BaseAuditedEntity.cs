using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Application.Contract
{
    public class BaseAuditedEntity
    {
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; } 
        public string? LastUpdatedBy { get; set; }
        public bool isDeleted { get; set; } 
        public DateTime? DeletedOn { get; set; } 
        public string? DeletedBy { get; set;}
    }
}
