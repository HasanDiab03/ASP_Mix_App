using System.ComponentModel.DataAnnotations;

namespace ASP_Mix_App.Models.BindingModels
{
	public class SearchModel
	{
		[Display(Name = "Search By Name Or Description")]
		public string? SearchTerm { get; set; }
		[Display(Name = "Category Name")]
		public int? Category { get; set; }
	}
}
