using EstateHelper.Application.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Domain.Models
{
    public class Product : BaseAuditedEntity
    {
        public string Id { get; set; }  = Guid.NewGuid().ToString();    
        public string Name { get; set; }    
        public string Description { get; set; }
        public string Location { get; set; }    
        public Pricing Pricing { get; set; }
        public int Size { get; set; }   
        public bool isAvailable {  get; set; }  
        public List<string> ImageLinks { get; set; }    
    }
}
