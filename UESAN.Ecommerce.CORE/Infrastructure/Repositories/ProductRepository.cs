using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UESAN.Ecommerce.CORE.Core.Entities;
using UESAN.Ecommerce.CORE.Core.Interfaces;
using UESAN.Ecommerce.CORE.Infrastructure.Data;

namespace UESAN.Ecommerce.CORE.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbContext _context;
        public ProductRepository(StoreDbContext context)
        {
            _context = context;
        }

        // Implement product-related data access methods here

        // GET Products
        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Product.Where(p => p.IsActive == true).ToListAsync();
        }

        // GET Product by ID
        public async Task<Product> GetProductById(int id)
        {
            return await _context.Product.Where(p => p.Id == id && p.IsActive == true).FirstOrDefaultAsync();
        }

        // INSERT Product
        public async Task<int> InsertProduct(Product product)
        {
            await _context.Product.AddAsync(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        // UPDATE Product
        public async Task UpdateProduct(Product product)
        {
            var existingProduct = await _context.Product.FindAsync(product.Id);
            if (existingProduct != null)
            {
                existingProduct.Description = product.Description;
                existingProduct.ImageUrl = product.ImageUrl;
                existingProduct.Stock = product.Stock;
                existingProduct.Price = product.Price;
                existingProduct.Discount = product.Discount;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.IsActive = product.IsActive;
                await _context.SaveChangesAsync();
            }
        }

        // DELETE Product
        public async Task DeleteProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
                await _context.SaveChangesAsync();
            }
        }


    }
}
