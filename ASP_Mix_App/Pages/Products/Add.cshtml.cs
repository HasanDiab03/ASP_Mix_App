using ASP_Mix_App.Models.BindingModels;
using ASP_Mix_App.Models.ViewModels;
using ASP_Mix_App.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP_Mix_App.Pages.Products
{
    public class AddModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public AddModel(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        [BindProperty]
        public ProductBindingModel ProductBindingModel { get; set; }
        public ProductViewModel ProductViewModel { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public async Task OnGetAsync()
        {
            Categories = (await _categoryService.GetAllCategories())
                .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    })
                .ToList();
        }       
        public async Task<IActionResult> OnPostAsync()
        {
            var category = await _categoryService.GetCategoryById(ProductBindingModel.CategoryId);
            if (category is null)
            {
                ModelState.AddModelError(string.Empty, "No Category With this Id exists");
            }
            if(ProductBindingModel.Photo is null)
			{
				ModelState.AddModelError(string.Empty, "Photo is required");
			}
            Console.WriteLine(string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
            if(!ModelState.IsValid)
            {
                return Page();
            }
            var id = await _productService.AddProduct(ProductBindingModel);
            await _productService.AddPhoto(id, ProductBindingModel.Photo);
            return RedirectToPage("./Index");
        }
    }
}
