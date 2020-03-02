using Microsoft.EntityFrameworkCore;
using OnlineShop_AspNetCore_ReactJS.Data.Entities;

namespace OnlineShop_AspNetCore_ReactJS.Data
{
    public class OnlineShopContext : DbContext
    {
        public OnlineShopContext(DbContextOptions<OnlineShopContext> options) : base(options)
        {

        }

        public DbSet<Banner> Banner { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<Pie> Pie { get; set; }
    }
}
