using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyStreeTBackend.Data;
using MyStreeTBackend.Models;

namespace MyStreeTBackend.Repo.RepoImpl
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Product>> GetProductsByBrandAsync(string brand)
        {
            return await _context.Products
                .Where(p => p.Brand.Contains(brand))
                .ToListAsync();
        }

        public async Task<List<Product>> GetProductsBySizeAsync(string size)
        {
            return await _context.Products
                .Where(p => p.Size.Contains(size))
                .ToListAsync();
        }

        public async Task<List<Product>> GetProductsFilteredAsync(string brand = null, string size = null)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(brand))
            {
                query = query.Where(p => p.Brand.Contains(brand));
            }

            if (!string.IsNullOrEmpty(size))
            {
                query = query.Where(p => p.Size.Contains(size));
            }

            return await query.ToListAsync();
        }
    }
}