using EstateHelper.Application.Contract;
using EstateHelper.Domain.HelperFunctions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Domain.Models
{
    public class Pricing
    {
        public decimal Price { get; set; }  
        public ProductUnitEnum Unit { get; set; }
        public decimal? Survey {  get; set; }
        public decimal? Development {  get; set; }
        public decimal Total => Price + (Survey ?? 0) + (Development ?? 0);
        [Key, ForeignKey("Product")]
        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}
