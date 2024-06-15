using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Application.Contract.Dtos.Products
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public PricingDto Pricing { get; set; }
        [Required(ErrorMessage = "Size is required")]
        public int Size { get; set; }
        [Required(ErrorMessage = "Availability is required")]
        public bool isAvailable { get; set; }
        
        public List<string> ImageLinks { get; set; }
    }
}
