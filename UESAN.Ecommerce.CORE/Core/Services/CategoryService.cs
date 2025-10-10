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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        //GET CATEGORY
        public async Task<IEnumerable<CategoryListDTO>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetCategories();
            var categoryDTOS = new List<CategoryListDTO>();

            foreach (var category in categories)
            {
                var categoryDTO = new CategoryListDTO();
                categoryDTO.Id = category.Id;
                categoryDTO.Description = category.Description;
                categoryDTOS.Add(categoryDTO);
            }

            return categoryDTOS;
        }

        //GET CATEGORY BY ID
        public async Task<CategoryListDTO> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                // Puedes lanzar una excepción o devolver un nuevo objeto por defecto
                throw new KeyNotFoundException($"No se encontró la categoría con id {id}.");
            }
            var categoryDTO = new CategoryListDTO();
            categoryDTO.Id = category.Id;
            categoryDTO.Description = category.Description;
            return categoryDTO;
        }

        //INSERT CATEGORY
        public async Task<int> InsertCategory(CategoryCreateDTO categoryCreateDTO)
        {
            var category = new Category();
            category.Description = categoryCreateDTO.Description;
            category.IsActive = true; // Por defecto, al crear una categoría, se establece como activa
            var newCategoryId = await _categoryRepository.InsertCategory(category);

            return newCategoryId;
        }

        //UPDATE CATEGORY
        public async Task UpdateCategory(CategoryListDTO categoryListDTO)
        {
            var category = new Category();
            category.Id = categoryListDTO.Id;
            category.Description = categoryListDTO.Description;
            await _categoryRepository.UpdateCategory(category);
        }

        //DELETE CATEGORY
        public async Task DeleteCategory(int id)
        {
            await _categoryRepository.DeleteCategory(id);
        }

        //DELETE LOGIC CATEGORY
        public async Task DeleteCategoryLogic(int id)
        {
            await _categoryRepository.DeleteCategoryLogic(id);
        }

    }
}
