using ASP_Mix_App.Models.BindingModels;
using ASP_Mix_App.Models.DataModels;
using ASP_Mix_App.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP_Mix_App.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public IndexModel(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public List<Product> Products { get; set; }
        [BindProperty(SupportsGet = true)]
        public SearchModel SearchModel { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public async Task OnGetAsync()
        {
            Products = await _productService.GetAllProducts(SearchModel);
            Categories = (await _categoryService.GetAllCategories())
                .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    })
                .ToList();
        }
    }
}
