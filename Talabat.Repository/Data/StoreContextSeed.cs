using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

using Talabat.Core.Entities;
using Talabat.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(AppDbContext context)
        {

            // Seed Brands
            if (!context.ProductBrands.Any())
            {
                var brands = GetProductBrands();
                await context.ProductBrands.AddRangeAsync(brands);
                await context.SaveChangesAsync();
            }

            // Seed Categories
            if (!context.ProductCategories.Any())
            {
                var categories = GetProductCategories();
                await context.ProductCategories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }

            // Seed Products
            if (!context.Products.Any())
            {
                var products = GetProducts();
                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }

        private static List<ProductBrand> GetProductBrands()
        {
            return new List<ProductBrand>
            {
                new ProductBrand { Id = 1, Name = "Nike" },
                new ProductBrand { Id = 2, Name = "Adidas" },
                new ProductBrand { Id = 3, Name = "Apple" },
                new ProductBrand { Id = 4, Name = "Samsung" },
                new ProductBrand { Id = 5, Name = "Sony" }
            };
        }

        private static List<ProductCategory> GetProductCategories()
        {
            return new List<ProductCategory>
            {
                new ProductCategory { Id = 1, Name = "Shoes" },
                new ProductCategory { Id = 2, Name = "Electronics" },
                new ProductCategory { Id = 3, Name = "Clothing" },
                new ProductCategory { Id = 4, Name = "Accessories" }
            };
        }

        private static List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Nike Air Max 270",
                    Description = "Comfortable running shoes with responsive cushioning.",
                    Price = 1200,
                    PictureUrl = "images/products/nike-airmax.jpg",
                    BrandId = 1,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Adidas Ultraboost",
                    Description = "High-performance running shoes with energy return.",
                    Price = 1500,
                    PictureUrl = "images/products/adidas-ultraboost.jpg",
                    BrandId = 2,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 3,
                    Name = "iPhone 14",
                    Description = "Latest Apple smartphone.",
                    Price = 45000,
                    PictureUrl = "images/products/iphone14.jpg",
                    BrandId = 3,
                    CategoryId = 2
                }
            };
        }
    }
}

