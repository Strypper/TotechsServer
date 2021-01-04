using AutoMapper;
using Intranet.DataObject;
using Intranet.Entities.Entities;

namespace Intranet
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Food, FoodDTO>()
                .ReverseMap()
                .ForMember(d => d.Id, o => o.Ignore());

            CreateMap<User, UserDTO>()
                .ReverseMap()
                .ForMember(d => d.Id, o => o.Ignore());

            CreateMap<UserFood, UserFoodDTO>()
                .ReverseMap()
                .ForMember(d => d.Id, o => o.Ignore());
        }
    }
}
