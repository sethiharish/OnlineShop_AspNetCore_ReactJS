using Microsoft.EntityFrameworkCore;
using OnlineShop_AspNetCore_ReactJS.Data;
using OnlineShop_AspNetCore_ReactJS.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop_AspNetCore_ReactJS.Services
{
    public class BannerService : IBannerService
    {
        private readonly OnlineShopContext context;

        public BannerService(OnlineShopContext context)
        {
            this.context = context;
        }

        public async Task<Banner> GetBannerAsync(int bannerId)
        {
            return await context.Banner.FindAsync(bannerId);
        }

        public async Task<IEnumerable<Banner>> GetBannersAsync()
        {
            return await context.Banner.ToListAsync();
        }
    }
}
