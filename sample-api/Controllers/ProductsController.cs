using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sample_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample_api.Controllers
{
    // http://localhost:5000/api/Products
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IStoreRepository _repository;

        public ProductsController(IStoreRepository repository)
        {
            _repository = repository;
        }
        // GET :  api/products
        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return _repository.GetProducts();
        }

        // GET By Id:api/products/{id}
        [HttpGet("{id}")]
        public Product GetProductById([FromRoute] int id)
        {
            return _repository.GetProductById(id);
        }

        //// POST: api/products
        [HttpPost]
        public void SaveProduct([FromBody] Product product)
        {
            _repository.SaveProduct(product);
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public void DeleteProduct([FromRoute] int id)
        {
            _repository.DeleteProduct(id);
        }

        // PUT: api/products/{id}
        [HttpPut]
        public void UpdateProduct([FromBody] Product product)
        {
            _repository.UpdateProduct(product);
        }
    }
}
