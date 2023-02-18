using Intranet.Contract;
using Intranet.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Repo;

public class UserConversationRepository : BaseRepository<UserConversation>, IUserConversationRepository
{
    public UserConversationRepository(IntranetContext ic) : base(ic) { }

    public override async Task<UserConversation?> FindByIdAsync(int userConversationId, CancellationToken cancellationToken = default)
        => await FindAll(uc => uc.Id == userConversationId).FirstOrDefaultAsync(cancellationToken);

    public async Task<IEnumerable<UserConversation?>> FindByUserId(string userId, CancellationToken cancellationToken = default)
        => await FindAll(uc => uc.User.Id.Equals(userId)).ToListAsync(cancellationToken);

    public async Task<IEnumerable<UserConversation?>> FindByConversationId(int conversationId, CancellationToken cancellationToken = default)
        => await FindAll(uc => uc.Conversation.Id == conversationId).ToListAsync(cancellationToken);
}
