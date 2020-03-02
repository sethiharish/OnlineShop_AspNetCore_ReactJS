using Microsoft.EntityFrameworkCore;
using OnlineShop_AspNetCore_ReactJS.Data;
using OnlineShop_AspNetCore_ReactJS.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop_AspNetCore_ReactJS.Services
{
    public class PieService : IPieService
    {
        private readonly OnlineShopContext context;

        public PieService(OnlineShopContext context)
        {
            this.context = context;
        }

        public async Task<Pie> GetPieAsync(int pieId)
        {
            return await context.Pie.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == pieId);
        }

        public async Task<IEnumerable<Pie>> GetPiesAsync()
        {
            return await context.Pie.Include(p => p.Category).ToListAsync();
        }

        public async Task<IEnumerable<Pie>> GetPiesOfTheWeekAsync()
        {
            return await context.Pie.Where(p => p.IsPieOfTheWeek).Include(p => p.Category).ToListAsync();
        }
    }
}
