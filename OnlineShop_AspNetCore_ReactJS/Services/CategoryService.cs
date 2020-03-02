using Microsoft.EntityFrameworkCore;
using OnlineShop_AspNetCore_ReactJS.Data;
using OnlineShop_AspNetCore_ReactJS.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop_AspNetCore_ReactJS.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly OnlineShopContext context;

        public CategoryService(OnlineShopContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await context.Category.ToListAsync();
        }

        public async Task<Category> GetCategoryAsync(int categoryId)
        {
            return await context.Category.FindAsync(categoryId);
        }
    }
}
