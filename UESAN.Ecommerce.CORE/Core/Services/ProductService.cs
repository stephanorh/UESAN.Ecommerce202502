using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UESAN.Ecommerce.CORE.Core.DTOs;
using UESAN.Ecommerce.CORE.Core.Entities;
using UESAN.Ecommerce.CORE.Core.Interfaces;

namespace UESAN.Ecommerce.CORE.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET ALL PRODUCTS
        public async Task<IEnumerable<ProductListDTO>> GetAllProducts()
        {
            var products = await _productRepository.GetAllProducts();
            var productDTOs = new List<ProductListDTO>();
            foreach (var product in products)
            {
                productDTOs.Add(new ProductListDTO
                {
                    Id = product.Id,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock,
                    ImageUrl = product.ImageUrl,
                    Discount = product.Discount,
                    CategoryId = product.CategoryId
                });
            }
            return productDTOs;
        }

        // GET PRODUCT BY ID
        public async Task<ProductListDTO> GetProductById(int id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null) return null;
            return new ProductListDTO
            {
                Id = product.Id,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                ImageUrl = product.ImageUrl,
                Discount = product.Discount,
                CategoryId = product.CategoryId
            };
        }

        // INSERT PRODUCT
        public async Task<int> InsertProduct(ProductCreateDTO productCreateDTO)
        {
            var product = new Product
            {
                Description = productCreateDTO.Description,
                ImageUrl = productCreateDTO.ImageUrl,
                Stock = productCreateDTO.Stock,
                Price = productCreateDTO.Price,
                Discount = productCreateDTO.Discount,
                CategoryId = productCreateDTO.CategoryId,
                IsActive = true
            };
            return await _productRepository.InsertProduct(product);
        }

        // UPDATE PRODUCT
        public async Task UpdateProduct(ProductListDTO productListDTO)
        {
            var product = new Product
            {
                Id = productListDTO.Id,
                Description = productListDTO.Description,
                ImageUrl = productListDTO.ImageUrl,
                Stock = productListDTO.Stock,
                Price = productListDTO.Price,
                Discount = productListDTO.Discount,
                CategoryId = productListDTO.CategoryId
            };
            await _productRepository.UpdateProduct(product);
        }

        // DELETE PRODUCT
        public async Task DeleteProduct(int id)
        {
            await _productRepository.DeleteProduct(id);
        }


    }
}
