using AutoMapper;
using Entities = OnlineShop_AspNetCore_ReactJS.Data.Entities;

namespace OnlineShop_AspNetCore_ReactJS.Profiles
{
    public class IterationProfile : Profile
    {
        public IterationProfile()
        {
            CreateMap<Entities.Iteration, Models.Iteration>();
        }
    }
}
