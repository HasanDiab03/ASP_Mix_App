using ASP_Mix_App.Data;
using ASP_Mix_App.Models.BindingModels;
using ASP_Mix_App.Models.DataModels;
using ASP_Mix_App.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ASP_Mix_App.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddCategory(CategoryBindingModel addedCategory)
        {
            var category = new Category
            {
                Name = addedCategory.Name,
            };
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();
            return category.Id;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category is null)
                return false;
            _context.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            // for some reson, when using any Async method, it is not working. so i did not use async methods of Find here
            return _context.Categories.Find(id);
        }

        public async Task<Category?> UpdateCategory(CategoryBindingModel updatedCategory, int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category is null)
                return null;
            category.Name = updatedCategory.Name;
            await _context.SaveChangesAsync();
            return category;


        }
    }
}
