using Intranet.Contract;
using Intranet.Entities.Database;
using Intranet.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Repo
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IntranetContext ic): base(ic){}

        public async Task<User> FindByUserIdWithoutCancellationToken(string id)
            => await FindAll(u => u.Id == id).FirstOrDefaultAsync();

        public async Task<User> FindByUserName(string userName, CancellationToken cancellationToken = default)
            => await FindAll(u => u.UserName == userName).FirstOrDefaultAsync(cancellationToken);

        public async Task<User> FindBySignalRConnectionId(string connectionId, CancellationToken cancellationToken = default)
            => await FindAll(u => u.SignalRConnectionId == connectionId).FirstOrDefaultAsync(cancellationToken);

        public async Task<User> FindByGuid(string guid, CancellationToken cancellationToken = default)
            => await FindAll(u => u.Guid == guid).FirstOrDefaultAsync(cancellationToken);
    }
}
