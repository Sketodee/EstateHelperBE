using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Application.Contract.Dtos.Products
{
    public class GetPricingDto
    {
        public decimal Price { get; set; }
        public PricingDuration Duration { get; set; }
        public decimal? Survey { get; set; }
        public decimal? Development { get; set; }
        public decimal Total { get; set; }
    }
}
