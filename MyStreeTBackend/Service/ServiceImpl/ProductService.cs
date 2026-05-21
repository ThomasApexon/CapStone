using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStreeTBackend.Models;
using MyStreeTBackend.Repo;

namespace MyStreeTBackend.Service.ServiceImpl
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            product.Id = Guid.NewGuid();
            return await _productRepository.CreateProductAsync(product);
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            return await _productRepository.UpdateProductAsync(product);
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            return await _productRepository.DeleteProductAsync(id);
        }

        public async Task<List<Product>> GetProductsByBrandAsync(string brand)
        {
            return await _productRepository.GetProductsByBrandAsync(brand);
        }

        public async Task<List<Product>> GetProductsBySizeAsync(string size)
        {
            return await _productRepository.GetProductsBySizeAsync(size);
        }

        public async Task<List<Product>> GetProductsFilteredAsync(string brand = null, string size = null)
        {
            return await _productRepository.GetProductsFilteredAsync(brand, size);
        }
    }
}