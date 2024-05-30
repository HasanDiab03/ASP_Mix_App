using ASP_Mix_App.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace ASP_Mix_App.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions opt) : base(opt)
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
