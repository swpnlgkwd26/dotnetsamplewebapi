using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sample_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample_api.Controllers
{
    // http://localhost:5001/api/Products
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // GET :  api/products
        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return new List<Product>
            {
                 new Product{ ProductID=1,Name="Chess"},
                 new Product{ ProductID=2,Name="Cricket Bat"}
            };
        }

        // GET By Id:api/products/{id}
        [HttpGet("{id}")]
        public Product GetProductById(int id)
        {
            return new Product
            {
                Name = "Product " + id
            };
        }
    }
}
