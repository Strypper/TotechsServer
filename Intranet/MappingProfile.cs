using Newtonsoft.Json;

namespace Intranet
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Food, FoodDTO>()
                .ReverseMap();

            CreateMap<Conversation, ConversationDTO>()
                .ReverseMap();

            CreateMap<Contribution, ContributionDTO>()
                .ReverseMap();

            CreateMap<Conversation, ConversationDirectModeDTO>();

            CreateMap<ChatMessage, ChatMessageDTO>()
                .ReverseMap();

            CreateMap<Project, ProjectDTO>()
                .ReverseMap();

            CreateMap<MeetingSchedule, MeetingScheduleDTO>()
                .ReverseMap();

            CreateMap<MeetingInfo, MeetingInfoDTO>()
                .ReverseMap();

            CreateMap<Attendance, AttendanceDTO>()
                .ReverseMap();

            CreateMap<TodoTask, TodoTaskDTO>()
                .ReverseMap();

            CreateMap<ImportanceLevel, ImportanceLevelDTO>()
                .ReverseMap();

            CreateMap<User, UserDTO>()
                .ForMember(d => d.Guid, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Skills, o => o.MapFrom(s => s.Skills == null ? null : JsonConvert.DeserializeObject<List<SkillDTO>>(s.Skills)));

            CreateMap<UserDTO, User>()
                .ForMember(d => d.Skills, o => o.MapFrom(s => s.Skills == null ? null : JsonConvert.SerializeObject(s.Skills)));

            CreateMap<UserFood, UserFoodDTO>()
                .ReverseMap();

            CreateMap<UserProject, UserProjectDTO>()
                .ReverseMap();

            CreateMap<Feature, FeatureDTO>()
                .ReverseMap();

            CreateMap<SubFeature, SubFeatureDTO>()
                .ReverseMap();

            CreateMap<ProjectTask, ProjectTaskDTO>()
                .ReverseMap();

            CreateMap<MediaAssets, MediaAssetsDTO>()
                .ReverseMap();
        }
    }
}
