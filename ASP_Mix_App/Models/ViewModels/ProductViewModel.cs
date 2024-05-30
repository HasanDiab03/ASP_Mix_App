using System.ComponentModel.DataAnnotations;

namespace ASP_Mix_App.Models.ViewModels
{
    public class ProductViewModel
    {
        [Required]
        [Display (Name = "Product Name")]
        public string Name { get; set; }
        [Display (Name = "Product Description")]
        public string Description { get; set; }
        [Required]
        [Display (Name = "Product Price")]
        public decimal Price { get; set; }
        [Required]
        [Display (Name = "Product Category Id")]
        public int CategoryId { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
