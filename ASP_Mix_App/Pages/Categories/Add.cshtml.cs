using ASP_Mix_App.Models.BindingModels;
using ASP_Mix_App.Models.ViewModels;
using ASP_Mix_App.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_Mix_App.Pages.Categories
{
    public class AddModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public AddModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [BindProperty]
        public CategoryBindingModel CategoryBindingModel { get; set; }
        public CategoryViewModel CategoryViewModel { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            var id = await _categoryService.AddCategory(CategoryBindingModel);
            return RedirectToPage("Index");

        }
    }
}
