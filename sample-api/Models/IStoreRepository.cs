using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample_api.Models
{
    public interface IStoreRepository
    {
        // Bring All the Products From Database
        IAsyncEnumerable<Product> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task SaveProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task UpdateProductAsync(Product product);
    }
}
