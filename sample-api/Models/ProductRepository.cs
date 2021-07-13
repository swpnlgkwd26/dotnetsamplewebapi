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

        public void DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public Product GetProductById(int id)
        {
            // _context.Products =  Data Collection
            return _context.Products.Find(id);
        }
        public List<Product> GetProducts()
        {
            return _context.Products.ToList();
        }
        public void SaveProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
    }
}
