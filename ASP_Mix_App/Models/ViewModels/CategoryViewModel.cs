using System.ComponentModel.DataAnnotations;

namespace ASP_Mix_App.Models.ViewModels
{
    public class CategoryViewModel
    {
        [Required]
        [Display(Name = "Category Name")]
        public string Name { get; set; }
    }
}
