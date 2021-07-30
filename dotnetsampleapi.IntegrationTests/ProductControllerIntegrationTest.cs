using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using sample_api;
using sample_api.Models;
using sample_api.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace dotnetsampleapi.IntegrationTests
{
    public class ProductControllerIntegrationTest : IClassFixture<TestWebFactory<Startup>>
    {
        // Get , Post , PUT , DELET
        private readonly HttpClient _client;
        // Logic =>  APIS =>  In Memory DB
        // HTTP Call=> HttpClient =>  API =>  In Memory DB

        public ProductControllerIntegrationTest(TestWebFactory<Startup> factory)
        {
            // Create Object of HttpClient
            _client = factory.CreateClient();
            ProductBindingTarget productBindingTarget = new ProductBindingTarget
            {
                Name = "Cricket Ball",
                Category = "Cricket",
                Description = "Cricket",
                MfgDate = DateTime.Now,
                Price = 100
            };
            // Convert productBindingTarget into the format that Post Method Expects
            var myContent = JsonConvert.SerializeObject(productBindingTarget);
            var buffer = Encoding.UTF8.GetBytes(myContent); // Bytes 

            // Byt array Content
            var byteArrayContent = new ByteArrayContent(buffer);
            byteArrayContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");


            // Add Mock Data to  In Memory Database through SaveProduct
            // HttpContent =>  ByteArrayContent (Bytes) : [bytes]
            var response = _client.PostAsync("/api/products", byteArrayContent).Result;
            response.EnsureSuccessStatusCode();


        }


        [Fact]
        public async Task GetProducts_Called()
        {
            var response = await _client.GetAsync("/api/products");
            response.EnsureSuccessStatusCode();

            var responseString =await response.Content.ReadAsStringAsync();

            Assert.Contains("Cricket", responseString);

            var products = JsonConvert.DeserializeObject<Product[]>(responseString);
            Assert.Equal("Cricket Ball", products[0].Name);


        }

        [Fact]
        public async Task CheckUpdateProduct_Called()
        {
            Product  product = new Product
            {
                ProductID =1,
                Name = "Cricket Ball",
                Category = "Cricket",
                Description = "Cricket",
                MfgDate = DateTime.Now,
                Price = 200
            };
            var myContent = JsonConvert.SerializeObject(product);
            var buffer = Encoding.UTF8.GetBytes(myContent); // Bytes 

            // Byt array Content
            var byteArrayContent = new ByteArrayContent(buffer);
            byteArrayContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response =await _client.PutAsync("/api/products", byteArrayContent);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Cricket", responseString);


            var products = JsonConvert.DeserializeObject<Product>(responseString);
            Assert.Equal(200, products.Price);

        }
    }
}
