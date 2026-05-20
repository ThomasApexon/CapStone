using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStreeTBackend.Models;

namespace MyStreeTBackend.Repo
{
    public interface IProductRepository
    {
        // Define methods for product CRUD operations
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(Guid id);
        Task<bool> AddProductAsync(Product product);  
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(Guid id);
    }
}