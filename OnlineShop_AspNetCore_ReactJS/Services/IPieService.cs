using OnlineShop_AspNetCore_ReactJS.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop_AspNetCore_ReactJS.Services
{
    public interface IPieService
    {
        Task<Pie> GetPieAsync(int pieId);

        Task<IEnumerable<Pie>> GetPiesAsync();

        Task<IEnumerable<Pie>> GetPiesOfTheWeekAsync();
    }
}
