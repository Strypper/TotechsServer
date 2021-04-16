using Intranet.Contract;
using Intranet.Entities.Database;
using Intranet.Entities.Entities;

namespace Intranet.Repo
{
    public class ChatMessageRepository : BaseRepository<ChatMessage>, IChatMessageRepository
    {
        public ChatMessageRepository(IntranetContext ic) : base(ic) { }
    }
}
