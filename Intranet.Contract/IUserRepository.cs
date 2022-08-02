using Intranet.Entities.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Contract
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> FindByUserIdWithoutCancellationToken(string id);
        Task<User> FindByUserName(string userName, CancellationToken cancellationToken = default);
        Task<User> FindBySignalRConnectionId(string connectionId, CancellationToken cancellationToken = default);
    }
}
