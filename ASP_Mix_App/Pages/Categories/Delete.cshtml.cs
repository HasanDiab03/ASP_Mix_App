using ASP_Mix_App.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_Mix_App.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public DeleteModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _categoryService.DeleteCategory(Id);
            if (result)
                return RedirectToPage("Index");
            return Page();
        }
    }
}
