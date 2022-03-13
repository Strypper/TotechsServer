using AutoMapper;
using Intranet.DataObject;
using Intranet.Entities.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Intranet
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Food, FoodDTO>()
                .ReverseMap()
                .ForMember(d => d.Id, o => o.Ignore());

            CreateMap<Conversation, ConversationDTO>()
                .ReverseMap()
                .ForMember(d => d.Id, o => o.Ignore());


            CreateMap<Conversation, ConversationDirectModeDTO>()
                .ForMember(d => d.Id, o => o.Ignore());

            CreateMap<ChatMessage, ChatMessageDTO>()
                .ReverseMap()
                .ForMember(d => d.Id, o => o.Ignore());

            CreateMap<Project, ProjectDTO>()
                .ReverseMap()
                .ForMember(d => d.Id, o => o.Ignore());

            CreateMap<User, UserDTO>()
                .ForMember(d => d.Skills, o => o.MapFrom(s => s.Skills == null ? null :         JsonConvert.DeserializeObject<List<SkillDTO>>(s.Skills)))
                .ForMember(d => d.Password, o => o.Ignore());

            CreateMap<UserDTO, User>()
                .ForMember(d => d.Skills, o => o.MapFrom(s => s.Skills == null ? null : JsonConvert.SerializeObject(s.Skills)))
                .ForMember(d => d.Id, o => o.Ignore());

            CreateMap<UserFood, UserFoodDTO>()
                .ReverseMap()
                .ForMember(d => d.Id, o => o.Ignore());

            CreateMap<UserProject, UserProjectDTO>()
                .ReverseMap()
                .ForMember(d => d.Id, o => o.Ignore());
        }
    }
}
