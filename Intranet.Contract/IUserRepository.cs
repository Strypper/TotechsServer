using Intranet.Entities.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Contract
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> FindByUserIdWithoutCancellationToken(int id);
        Task<User> FindByUserName(string userName, CancellationToken cancellationToken = default);
        Task<User> FindBySignalRConnectionId(string connectionId, CancellationToken cancellationToken = default);
        Task<User> FindByGuid(string guid, CancellationToken cancellationToken = default);
    }
}
