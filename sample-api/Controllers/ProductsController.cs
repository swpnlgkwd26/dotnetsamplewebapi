﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sample_api.Models;
using sample_api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
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
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public  IActionResult GetProducts()
        {        
            var products = _repository.GetProductsAsync();
            if (products ==  null)
            {
                return NotFound();
            }
            return Ok(products);
        }


        // GET By Id:api/products/{id}
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            await _repository.DeleteProductAsync(id);
            return Ok();
        }



        // PUT: api/products/{id}
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
