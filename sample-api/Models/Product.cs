using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sample_api.Models
{
    public class Product
    {
        [Key]       
        public int ProductID { get; set; }
        [Required]
        public string  Name { get; set; }
        [Required]
        public string  Description { get; set; }
        [Required]
        public int  Price { get; set; }
        [Required]
        public string  Category { get; set; }
        [Required]
        public DateTime MfgDate { get; set; }
        
    }
}
