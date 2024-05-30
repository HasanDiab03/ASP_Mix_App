using ASP_Mix_App.Models.DataModels;
using ASP_Mix_App.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_Mix_App.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public IndexModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public List<Category> Categories { get; set; }
        public async Task OnGetAsync()
        {
            Categories = await _categoryService.GetAllCategories();
        }
    }
}
