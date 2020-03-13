using AutoMapper;
using Entities = OnlineShop_AspNetCore_ReactJS.Data.Entities;

namespace OnlineShop_AspNetCore_ReactJS.Profiles
{
    public class WorkItemProfile : Profile
    {
        public WorkItemProfile()
        {
            CreateMap<Entities.WorkItem, Models.WorkItem>()
                .ForMember(dest => dest.IterationName, opt => opt.MapFrom(src => src.Iteration.Name));
        }
    }
}
