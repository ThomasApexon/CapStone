using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStreeTBackend.Models;

namespace MyStreeTBackend.Service
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(Guid id);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(Guid id);
        Task<List<Product>> GetProductsByBrandAsync(string brand);
        Task<List<Product>> GetProductsBySizeAsync(string size);
        Task<List<Product>> GetProductsFilteredAsync(string brand = null, string size = null);
    }
}