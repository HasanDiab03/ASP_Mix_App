using ASP_Mix_App.Filters;
using ASP_Mix_App.Models.BindingModels;
using ASP_Mix_App.Models.DataModels;
using ASP_Mix_App.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Mix_App.Extensions
{
    public static class EndpointsExtension
    {
        public static WebApplication AddEndpoints(this WebApplication app)
        {
            // Products
            var products = app.MapGroup("/api/products").WithTags("Products");
            products.MapGet("/", async (IProductService service, [AsParameters] SearchModel searchModel) => await service.GetAllProducts(searchModel));
            products.MapGet("/{id}", async (IProductService service, int id) =>
            {
                return HandleResponse(await service.GetProductById(id));
            });
            products.MapPost("/", async (IProductService service, ProductBindingModel product) => await service.AddProduct(product))
                .AddEndpointFilter<ValidateProduct>();
            products.MapPut("/{id}", async (IProductService service, ProductBindingModel product, int id) =>
            {
                return HandleResponse(await service.UpdateProduct(product, id));
            })
            .AddEndpointFilter<ValidateProduct>();
            products.MapDelete("/{id}", async (IProductService service, int id) =>
            {
                return HandleResponse(await service.DeleteProduct(id), true);
            });
            products.MapPost("/upload", async (IProductService service, int id, IFormFile image) =>
            {
                return HandleResponse(await service.AddPhoto(id, image));
            });

            // Categories
            var categories = app.MapGroup("/api/categories").WithTags("Categories");
            categories.MapGet("/", async (ICategoryService service) => await service.GetAllCategories());
            categories.MapGet("/{id}", async (ICategoryService service, int id) =>
            {
                return HandleResponse(await service.GetCategoryById(id));
            });
            categories.MapPost("/", async (ICategoryService service, CategoryBindingModel category) => await service.AddCategory(category));
            categories.MapPut("/{id}", async (ICategoryService service, CategoryBindingModel category, int id) =>
            {
                return HandleResponse(await service.UpdateCategory(category, id));
            });
            categories.MapDelete("/{id}", async (ICategoryService service, int id) =>
            {
                return HandleResponse(await service.DeleteCategory(id), true);
            });
            return app;
        }
        private static IResult HandleResponse<T>(T? entity, bool isDelete = false)
        {
            if(isDelete)
            {
                if(entity is true)
                {
                    return Results.Ok("Deleted Successfully");
                } else
                {
                    return Results.NotFound("Not Found");
                }
            }
            if(entity is null)
            {
                return Results.NotFound("Not Found");
            }
            return Results.Ok(entity);

        }
    }
}
