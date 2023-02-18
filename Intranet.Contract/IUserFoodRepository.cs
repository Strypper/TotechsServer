using Intranet.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Contract;

public interface IUserFoodRepository : IRepositoryBase<UserFood>
{
    Task<UserFood?> FindByUserId(string userId, CancellationToken cancellationToken);
}
