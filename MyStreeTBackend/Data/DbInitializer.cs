using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStreeTBackend.Data;
using MyStreeTBackend.Models;
using MyStreeTBackend.Utils;

namespace MyStreeTBackend.Data
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(AppDbContext context)
        {
            // Ensure database is created
            await context.Database.EnsureCreatedAsync();

            // Check if database has been seeded
            if (context.Users.Any() || context.Products.Any())
            {
                return;
            }

            // Seed Users
            var adminUser = new User
            {
                Id = Guid.NewGuid(),
                Email = "admin@mystreet.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                IsAdmin = true
            };

            var regularUser = new User
            {
                Id = Guid.NewGuid(),
                Email = "user@mystreet.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("User@123"),
                IsAdmin = false
            };

            context.Users.AddRange(adminUser, regularUser);
            await context.SaveChangesAsync();

            // Seed Products
            var products = new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Air Max 90",
                    Brand = "Nike",
                    Price = 119.99m,
                    Size = "7,8,9,10,11,12,13",
                    StockQty = 50,
                    ImageUrl = "https://via.placeholder.com/300?text=Air+Max+90",
                    Description = "Classic Nike Air Max 90. Comfortable and stylish sneaker for everyday wear."
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Air Force 1",
                    Brand = "Nike",
                    Price = 99.99m,
                    Size = "6,7,8,9,10,11,12,13",
                    StockQty = 75,
                    ImageUrl = "https://via.placeholder.com/300?text=Air+Force+1",
                    Description = "Legendary Nike Air Force 1. The iconic basketball shoe."
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Ultraboost 21",
                    Brand = "Adidas",
                    Price = 139.99m,
                    Size = "7,8,9,10,11,12",
                    StockQty = 35,
                    ImageUrl = "https://via.placeholder.com/300?text=Ultraboost+21",
                    Description = "Adidas Ultraboost 21 with responsive cushioning and energy return."
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Stan Smith",
                    Brand = "Adidas",
                    Price = 89.99m,
                    Size = "6,7,8,9,10,11,12,13",
                    StockQty = 60,
                    ImageUrl = "https://via.placeholder.com/300?text=Stan+Smith",
                    Description = "Classic Adidas Stan Smith. Timeless design and comfort."
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "New Balance 990v5",
                    Brand = "New Balance",
                    Price = 149.99m,
                    Size = "7,8,9,10,11,12",
                    StockQty = 40,
                    ImageUrl = "https://via.placeholder.com/300?text=990v5",
                    Description = "Premium New Balance 990v5. Engineered for comfort and performance."
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Chuck Taylor All Star",
                    Brand = "Converse",
                    Price = 59.99m,
                    Size = "4,5,6,7,8,9,10,11,12,13",
                    StockQty = 100,
                    ImageUrl = "https://via.placeholder.com/300?text=Chuck+Taylor",
                    Description = "Iconic Converse Chuck Taylor All Star. Perfect everyday casual shoe."
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Vans Old Skool",
                    Brand = "Vans",
                    Price = 69.99m,
                    Size = "6,7,8,9,10,11,12",
                    StockQty = 55,
                    ImageUrl = "https://via.placeholder.com/300?text=Vans+Old+Skool",
                    Description = "Classic Vans Old Skool with signature side stripe."
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Puma RS-X",
                    Brand = "Puma",
                    Price = 79.99m,
                    Size = "7,8,9,10,11,12,13",
                    StockQty = 45,
                    ImageUrl = "https://via.placeholder.com/300?text=Puma+RS-X",
                    Description = "Modern Puma RS-X with retro-styled aesthetics."
                }
            };

            context.Products.AddRange(products);
            await context.SaveChangesAsync();
        }
    }
}
