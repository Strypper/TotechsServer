using Intranet.Contract;
using Intranet.Entities.Database;
using Intranet.Entities.Entities;

namespace Intranet.Repo
{
    public class UserConversationRepository : BaseRepository<UserConversation>, IUserConversationRepository
    {
        public UserConversationRepository(IntranetContext ic) : base(ic) { }
    }
}
