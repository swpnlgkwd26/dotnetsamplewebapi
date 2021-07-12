using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample_api.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string  Name { get; set; }
        public string  Description { get; set; }
        public int  Price { get; set; }
        public string  Category { get; set; }
        public DateTime MfgDate { get; set; }
    }
}
