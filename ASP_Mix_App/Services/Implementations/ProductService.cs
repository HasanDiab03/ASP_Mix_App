using ASP_Mix_App.Data;
using ASP_Mix_App.Models.BindingModels;
using ASP_Mix_App.Models.DataModels;
using ASP_Mix_App.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ASP_Mix_App.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(AppDbContext context, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<int> AddProduct(ProductBindingModel addedProduct)
        {
            var product = new Product
            {
                Name = addedProduct.Name,
                Price = addedProduct.Price,
                Description = addedProduct.Description,
                CategoryId = addedProduct.CategoryId,
                PhotoUrl = ""
            };
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product is null)
                return false;
            _context.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Product>> GetAllProducts(SearchModel searchModel)
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();
            if (searchModel.SearchTerm is not null)
            {
                query = query.Where(p => p.Name.Contains(searchModel.SearchTerm) || p.Description.Contains(searchModel.SearchTerm));
            }
            if(searchModel.Category is not null)
            {
                query = query.Where(p => p.CategoryId == searchModel.Category);
            }
            return await query.ToListAsync();
        }

        public async Task<Product?> GetProductById(int id)
        {
            // for some reason, async methods are not working so i didn't use them
            var product = _context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
            return product;
        }

        public async Task<Product?> UpdateProduct(ProductBindingModel updatedProduct, int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product is null)
                return null;
            UpdateProduct(product, updatedProduct);
            await _context.SaveChangesAsync();
            return product;

        }
        private static void UpdateProduct(Product product, ProductBindingModel updatedProduct)
        {
            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.Description = updatedProduct.Description;
            product.CategoryId = updatedProduct.CategoryId;
        }
        public async Task<string?> AddPhoto(int id, IFormFile image)
        {
            var product = await _context.Products.FindAsync(id);
            if (product is null)
                return null;
            var fileExtension = Path.GetExtension(image.FileName);
            var guid = Guid.NewGuid();
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", "images", $"{image.FileName + guid}{fileExtension}");
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.CopyToAsync(stream);
            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}" +
                              $"{_httpContextAccessor.HttpContext.Request.PathBase}/images/{image.FileName + guid}{fileExtension}";
            product.PhotoUrl = urlFilePath;
            await _context.SaveChangesAsync();
            return urlFilePath;
        }
    }
}
