using System.ComponentModel.DataAnnotations;

namespace ASP_Mix_App.Models.BindingModels
{
    public record CategoryBindingModel
    {
        [Required]
        public string Name { get; set; }
    }
}
