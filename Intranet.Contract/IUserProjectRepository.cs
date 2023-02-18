using Intranet.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Contract;

public interface IUserProjectRepository : IRepositoryBase<UserProject>
{
    Task<UserProject?> FindByUserId(string userId, CancellationToken cancellationToken);
}
