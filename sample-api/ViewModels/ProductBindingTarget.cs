using sample_api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sample_api.ViewModels
{
    public class ProductBindingTarget
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public DateTime MfgDate { get; set; }

        public Product ToProduct() => new Product
        {
            Name= this.Name,
            Price = this.Price,
            Category =  this.Category,
            Description = this.Description,
            MfgDate = this.MfgDate
        };

    }
}
