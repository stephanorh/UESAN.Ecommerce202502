using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UESAN.Ecommerce.CORE.Core.DTOs;
using UESAN.Ecommerce.CORE.Core.Entities;
using UESAN.Ecommerce.CORE.Core.Interfaces;

namespace UESAN.Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        //private readonly ICategoryRepository _categoryRepository;
        //añadimos el ICategoryServices ya que ya no vamos a usar el _categoryRepository directamente
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> InsertCategory([FromBody] CategoryCreateDTO categoryCreateDTO)
        {
            if (categoryCreateDTO == null)
            {
                return BadRequest();
            }
            var newCategoryId = await _categoryService.InsertCategory(categoryCreateDTO);
            return CreatedAtAction(nameof(GetCategoryById), new { id = newCategoryId }, categoryCreateDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id
           , [FromBody] CategoryListDTO categoryListDTO)
        {
            if (categoryListDTO == null || categoryListDTO.Id != id)
            {
                return BadRequest();
            }
            var existingCategory = await _categoryService.GetCategoryById(id);
            if (existingCategory == null)
            {
                return NotFound();
            }
            await _categoryService.UpdateCategory(categoryListDTO);
            return NoContent();
        }
    


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var existingCategory = await _categoryService.GetCategoryById(id);
            if (existingCategory == null)
            {
                return NotFound();
            }
            await _categoryService.DeleteCategory(id);
            return NoContent();
        }

        [HttpDelete("logic/{id}")]
        public async Task<IActionResult> DeleteCategoryLogic(int id)
        {
            var existingCategory = await _categoryService.GetCategoryById(id);
            if (existingCategory == null)
            {
                return NotFound();
            }
            await _categoryService.DeleteCategoryLogic(id);
            return NoContent();
        }

        [HttpGet("all")]
        public IActionResult GetCategoriesAll()
        {
            var categories = _categoryService.GetAllCategories();
            return Ok(categories);
        }

        


    }
}
