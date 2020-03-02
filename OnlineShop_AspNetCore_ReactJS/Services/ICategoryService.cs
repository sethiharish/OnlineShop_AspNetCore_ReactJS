using OnlineShop_AspNetCore_ReactJS.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop_AspNetCore_ReactJS.Services
{
    public interface ICategoryService
    {
        Task<Category> GetCategoryAsync(int categoryId);

        Task<IEnumerable<Category>> GetCategoriesAsync();
    }
}
