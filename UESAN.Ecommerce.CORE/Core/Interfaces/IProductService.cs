﻿using UESAN.Ecommerce.CORE.Core.DTOs;

namespace UESAN.Ecommerce.CORE.Core.Interfaces
{
    public interface IProductService
    {
        Task DeleteProduct(int id);
        Task<IEnumerable<ProductListDTO>> GetAllProducts();
        Task<ProductListDTO> GetProductById(int id);
        Task<int> InsertProduct(ProductCreateDTO productCreateDTO);
        Task UpdateProduct(ProductListDTO productListDTO);
    }
}