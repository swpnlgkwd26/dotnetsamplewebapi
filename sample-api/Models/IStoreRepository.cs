using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample_api.Models
{
    public interface IStoreRepository
    {
        // Bring All the Products From Database
        List<Product> GetProducts();
        Product GetProductById(int id);
        void SaveProduct(Product product);
        void DeleteProduct(int id);

        void UpdateProduct(Product product);
    }
}
