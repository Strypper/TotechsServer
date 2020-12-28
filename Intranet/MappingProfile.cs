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
        }
    }
}
