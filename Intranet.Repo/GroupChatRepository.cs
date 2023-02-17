using Intranet.Contract;
using Intranet.Entities;

namespace Intranet.Repo;

public class GroupChatRepository : BaseRepository<GroupChat>, IGroupChatRepository
{
    public GroupChatRepository(IntranetContext ic) : base(ic) { }
}
