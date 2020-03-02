using OnlineShop_AspNetCore_ReactJS.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop_AspNetCore_ReactJS.Services
{
    public interface IBannerService
    {
        Task<Banner> GetBannerAsync(int bannerId);

        Task<IEnumerable<Banner>> GetBannersAsync();
    }
}
