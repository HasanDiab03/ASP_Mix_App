
using ASP_Mix_App.Models.BindingModels;
using ASP_Mix_App.Services.Interfaces;

namespace ASP_Mix_App.Filters
{
    public class ValidateProduct : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var product = context.GetArgument<ProductBindingModel>(1);
            if (string.IsNullOrEmpty(product.Name) || product.Price < 0)
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    {"Name", new[] {"Name is Required"} },
                    {"Price", new[] {"Price should be positive"} }
                }); 
            }
            var categoryService = context.HttpContext.RequestServices.GetRequiredService<ICategoryService>();
            var cat = await categoryService.GetCategoryById(product.CategoryId);
            if(cat is null)
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    {"CategoryId", new[] {"No Category with provided Id exists"} },
                });
            }
            return await next(context);
        }
    }
}
