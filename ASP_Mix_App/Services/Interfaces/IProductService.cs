using ASP_Mix_App.Models.BindingModels;
using ASP_Mix_App.Models.DataModels;

namespace ASP_Mix_App.Services.Interfaces
{
    public interface IProductService
    {
        public Task<List<Product>> GetAllProducts(SearchModel searchModel);
        public Task<Product?> GetProductById(int id);
        public Task<bool> DeleteProduct(int id);
        public Task<int> AddProduct(ProductBindingModel addedProduct);
        public Task<Product?> UpdateProduct(ProductBindingModel updatedProduct, int id);
        public Task<string?> AddPhoto(int id, IFormFile image);
    }
}
