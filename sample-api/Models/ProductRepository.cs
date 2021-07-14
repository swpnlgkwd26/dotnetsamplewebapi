using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample_api.Models
{
    public class ProductRepository : IStoreRepository
    {
        private readonly TataDBContext _context;

        // Data Context knows how to connect to db
        public ProductRepository(TataDBContext context)
        {
            _context = context;
        }

        // Async  Methods
        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id); 
            _context.Products.Remove(product);  // Not Blocking Operation for EF 
            await _context.SaveChangesAsync();  // Internal it saves in Queue and Executes on SaveChanges
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }        
        public IAsyncEnumerable<Product> GetProductsAsync()
        {
            return _context.Products;
        }
        public async Task SaveProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateProductAsync(Product product)
        {
            // Queue :  Update
            _context.Products.Update(product);// Not Blocking Operation for EF 
            await _context.SaveChangesAsync(); // Execute
        }
    }
}
