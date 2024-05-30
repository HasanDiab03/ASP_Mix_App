using ASP_Mix_App.Models.BindingModels;
using ASP_Mix_App.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_Mix_App.Pages.Categories
{
    public class EditModel : PageModel
    {
		private readonly ICategoryService _categoryService;

		public EditModel(ICategoryService categoryService)
        {
			_categoryService = categoryService;
		}
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        [BindProperty]
        public CategoryBindingModel CategoryBindingModel { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var cat = await _categoryService.GetCategoryById(Id);
            if (cat is null)
                return RedirectToPage("Index");
            CategoryBindingModel = new CategoryBindingModel
			{
				Name = cat.Name
			};
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			var result = await _categoryService.UpdateCategory(CategoryBindingModel, Id);
		    return RedirectToPage("Index");
		}
    }
}
