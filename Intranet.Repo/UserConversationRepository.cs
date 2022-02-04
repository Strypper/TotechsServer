using Intranet.Contract;
using Intranet.Entities.Database;
using Intranet.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Repo
{
    public class UserConversationRepository : BaseRepository<UserConversation>, IUserConversationRepository
    {
        public UserConversationRepository(IntranetContext ic) : base(ic) { }

        public override async Task<UserConversation> FindByIdAsync(int userConversationId, CancellationToken cancellationToken = default)
            => await FindAll(uc => uc.Id == userConversationId).FirstOrDefaultAsync(cancellationToken);

        public async Task<IEnumerable<UserConversation>> FindByUserId(int userId, CancellationToken cancellationToken = default)
            => await FindAll(uc => uc.User.Id == userId).ToListAsync(cancellationToken);
    }
}
