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

            CreateMap<Team, TeamDTO>()
                .ReverseMap()
                .ForMember(d => d.Id, o => o.Ignore());

            CreateMap<User, UserDTO>()
                .ForMember(d => d.Password, o => o.Ignore());

            CreateMap<UserDTO, User>()
                .ForMember(d => d.Id, o => o.Ignore());

            CreateMap<UserFood, UserFoodDTO>()
                .ReverseMap()
                .ForMember(d => d.Id, o => o.Ignore());
        }
    }
}
