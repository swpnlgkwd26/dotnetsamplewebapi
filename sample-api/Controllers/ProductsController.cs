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
        [Produces("application/json","application/xml")]
        public IAsyncEnumerable<Product> GetProducts()
        {
            return _repository.GetProductsAsync();
        }

        // GET By Id:api/products/{id}
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();

            }
            return Ok(new
            {
                ProductId = product.ProductID,
                ProductName = product.Name,
                Price = product.Price,
                Category = product.Category
            });
        }

        //// POST: api/products
        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> SaveProduct([FromBody] ProductBindingTarget productBindingTarget)
        {
            if (productBindingTarget == null)
            {
                return BadRequest("Model Cant not be Null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Model State is Invalid");
            }
            var targetProductObjct = productBindingTarget.ToProduct();
            await _repository.SaveProductAsync(targetProductObjct);
            return Ok();
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            await _repository.DeleteProductAsync(id);
            return Ok();
        }

        // PUT: api/products/{id}
        [HttpPut]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Model Cant not be null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is Invalid");
            }
            await _repository.UpdateProductAsync(product);
            return Ok();
        }
    }
}
