﻿using UESAN.Ecommerce.CORE.Core.Entities;

namespace UESAN.Ecommerce.CORE.Core.Interfaces
{
    public interface IProductRepository
    {
        Task DeleteProduct(int id);
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<int> InsertProduct(Product product);
        Task UpdateProduct(Product product);
    }
}