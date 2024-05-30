using ASP_Mix_App.Models.DataModels;
using ASP_Mix_App.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_Mix_App.Pages.Products
{
    public class DetailsModel : PageModel
    {
		private readonly IProductService _productService;

		public DetailsModel(IProductService productService)
        {
			_productService = productService;
		}
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public Product? Product { get; set; }
        public async void OnGet()
        {
            Product = await _productService.GetProductById(Id);
        }
    }
}
