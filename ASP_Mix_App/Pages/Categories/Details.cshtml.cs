using ASP_Mix_App.Models.DataModels;
using ASP_Mix_App.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_Mix_App.Pages.Categories
{
    public class DetailsModel : PageModel
    {
		private readonly ICategoryService _categoryService;

		public DetailsModel(ICategoryService categoryService)
        {
			_categoryService = categoryService;
		}
        public Category? Category { get; set; }
        public async void OnGet(int id)
        {
            Category = await _categoryService.GetCategoryById(id);
            Console.WriteLine(Category.Name);
        }
    }
}
