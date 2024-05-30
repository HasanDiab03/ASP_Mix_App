using ASP_Mix_App.Data;
using ASP_Mix_App.Services.Implementations;
using ASP_Mix_App.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ASP_Mix_App.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServicesToDI(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("Default"));
            });
            services.AddHttpContextAccessor();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            return services;
        }
    }
}
