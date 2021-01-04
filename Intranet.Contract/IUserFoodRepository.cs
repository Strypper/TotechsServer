using Intranet.Entities.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Contract
{
    public interface IUserFoodRepository : IRepositoryBase<UserFood>
    {
        Task<UserFood> FindByUserId(int userId, CancellationToken cancellationToken);
    }
}
