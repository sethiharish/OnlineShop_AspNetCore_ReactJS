using AutoMapper;
using Entities = OnlineShop_AspNetCore_ReactJS.Data.Entities;

namespace OnlineShop_AspNetCore_ReactJS.Profiles
{
    public class BannerProfile : Profile
    {
        public BannerProfile()
        {
            CreateMap<Entities.Banner, Models.Banner>();
        }
    }
}
