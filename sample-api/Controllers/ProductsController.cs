using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sample_api.Models;
using sample_api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample_api.Controllers
{
    // Async Controllers
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

        // Asynchronous  Action Methods
        // GET :  api/products
        [HttpGet]
        public IAsyncEnumerable<Product> GetProducts()
        {
            return _repository.GetProductsAsync();
        }

        // GET By Id:api/products/{id}
        [HttpGet("{id}")]
        public async Task<Product> GetProductById([FromRoute] int id)
        {
            return await _repository.GetProductByIdAsync(id);
        }    


        //// POST: api/products
        [HttpPost]
        public async Task SaveProduct([FromBody] ProductBindingTarget productBindingTarget)
        {
            // If ProductBindingTarget Null
            //if (productBindingTarget == null)
            //{
            //    //throw some Error();
            //}
            //if (!ModelState.IsValid(productBindingTarget))
            //{

            //}
            // Wrong Request

            var targetProductObjct = productBindingTarget.ToProduct();           
            await _repository.SaveProductAsync(targetProductObjct);
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public async Task DeleteProduct([FromRoute] int id)
        {
           await _repository.DeleteProductAsync(id);
        }

        // PUT: api/products/{id}
        [HttpPut]
        public async Task UpdateProduct([FromBody] Product product)
        {
            // Product Is Null
            // Product Object some Invalid Data
           await _repository.UpdateProductAsync(product);
        }
    }
}
