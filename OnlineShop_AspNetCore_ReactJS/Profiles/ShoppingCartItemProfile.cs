using AutoMapper;
using Entities = OnlineShop_AspNetCore_ReactJS.Data.Entities;

namespace OnlineShop_AspNetCore_ReactJS.Profiles
{
    public class ShoppingCartItemProfile : Profile
    {
        public ShoppingCartItemProfile()
        {
            CreateMap<Entities.ShoppingCartItem, Models.ShoppingCartItem>()
                .ForMember(dest => dest.PieName, opt => opt.MapFrom(src => src.Pie.Name))
                .ForMember(dest => dest.PiePrice, opt => opt.MapFrom(src => src.Pie.Price))
                .ForMember(dest => dest.PieThumbnailImageUrl, opt => opt.MapFrom(src => src.Pie.ThumbnailImageUrl));
        }
    }
}
