using Intranet.Entities.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Contract
{
    public interface IUserTeamRepository : IRepositoryBase<UserTeam>
    {
        Task<UserTeam> FindByUserId(int userId, CancellationToken cancellationToken);
    }
}
