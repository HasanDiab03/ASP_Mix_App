namespace ASP_Mix_App.Models.DataModels
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string PhotoUrl { get; set; }
        public int CategoryId { get; set; }
        // Navigation Property
        public Category Category { get; set; }
    }
}
