using Intranet.Contract;
using Intranet.Entities.Database;
using Intranet.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Repo
{
    public class UserFoodRepository : BaseRepository<UserFood>, IUserFoodRepository
    {
        public UserFoodRepository(IntranetContext ic) : base(ic)
        {

        }

        //public override Task<UserFood> FindAll(Expression<Func<UserFood, bool>>? predicate = null)
        //    => _dbSet.Where(predicate!).Include(f => f.Food).Include(u => u.User);

        public override async Task<UserFood> FindByIdAsync(int userFood, CancellationToken cancellationToken = default)
            => await FindAll(uf => uf.Id == userFood).Include(f => f.Food).Include(u => u.User).FirstOrDefaultAsync(cancellationToken);

        public async Task<UserFood> FindByUserId(int userId, CancellationToken cancellationToken = default)
            => await FindAll(uf => uf.User.Id == userId).Include(f => f.Food).Include(u => u.User).FirstOrDefaultAsync(cancellationToken);
    }
}
