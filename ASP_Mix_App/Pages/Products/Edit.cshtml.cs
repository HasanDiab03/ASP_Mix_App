using ASP_Mix_App.Models.BindingModels;
using ASP_Mix_App.Models.DataModels;
using ASP_Mix_App.Models.ViewModels;
using ASP_Mix_App.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;

namespace ASP_Mix_App.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public EditModel(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        [BindProperty]
        public ProductBindingModel ProductBindingModel { get; set; }
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var product = await _productService.GetProductById(Id);
            if(product is null)
			{
				return RedirectToPage("Index");
			}
            ProductBindingModel = new ProductBindingModel
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId
            };
            Categories = (await _categoryService.GetAllCategories())
                .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    })
                .ToList();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(IFormFile? photo)
        {
            var category = await _categoryService.GetCategoryById(ProductBindingModel.CategoryId);
            if (category is null)
            {
                ModelState.AddModelError(string.Empty, "No Category With this Id exists");
            }
            Console.WriteLine(string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (photo is not null)
            {
                var photoUrl = await _productService.AddPhoto(Id, photo);
            }
            await _productService.UpdateProduct(ProductBindingModel, Id);
            return RedirectToPage("Index");
        }
    }
}
