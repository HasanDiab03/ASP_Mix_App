using ASP_Mix_App.Models.BindingModels;
using ASP_Mix_App.Models.DataModels;

namespace ASP_Mix_App.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<List<Category>> GetAllCategories();
        public Task<Category?> GetCategoryById(int id);
        public Task<bool> DeleteCategory(int id);
        public Task<int> AddCategory(CategoryBindingModel addedCategory);
        public Task<Category?> UpdateCategory(CategoryBindingModel updatedCategory, int id);
    }
}
