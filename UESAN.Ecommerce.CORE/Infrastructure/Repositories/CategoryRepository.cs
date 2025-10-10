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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StoreDbContext _context;

        public CategoryRepository(StoreDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Category> GetCategoriesAll()
        {
            //var context = new StoreDbContext();
            //var categories = context.Category.ToList();

            //return categories;

            return _context.Category.ToList();
        }

        // Funcion asincrona
        // GET Category
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Category
                .Where(c => c.IsActive == true)
                .ToListAsync();
        }

        // GET Category 
        public async Task<Category?> GetCategoryById(int id)
        {
            return await _context.Category
                .Where(c => c.Id == id && c.IsActive == true)
                .FirstOrDefaultAsync();
        }


        // INSERT Category
        public async Task<int> InsertCategory(Category category)
        {
            await _context.Category.AddAsync(category);
            int rows = await _context.SaveChangesAsync();
            return category.Id;
        }

        // UPDATE Category
        public async Task UpdateCategory(Category category)
        {
            var categoryExist = await _context.Category.FindAsync(category.Id);
            if(categoryExist != null)
            {
                categoryExist.Description = category.Description;
                categoryExist.IsActive = categoryExist.IsActive;
            }
            await _context.SaveChangesAsync();
        }

        //DELETE convencional
        public async Task DeleteCategory(int id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category != null)
            {

                _context.Category.Remove(category);
                int rows = await _context.SaveChangesAsync();
            }
        }

        //SOFT DELETE (Actualiar su estado)
        public async Task DeleteCategoryLogic(int id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category != null)
            {
                category.IsActive = false;
                _context.Category.Update(category);
                int rows = await _context.SaveChangesAsync();
            }
        }



    }
}
