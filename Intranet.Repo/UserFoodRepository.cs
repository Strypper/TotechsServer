using Intranet.Contract;
using Intranet.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Repo;

public class UserFoodRepository : BaseRepository<UserFood>, IUserFoodRepository
{
    public UserFoodRepository(IntranetContext ic) : base(ic) { }

    public override IQueryable<UserFood> FindAll(Expression<Func<UserFood, bool>>? predicate = null)
        => (predicate == null ? _dbSet.Where(x => true) : _dbSet.Where(predicate)).Include(f => f.Food).Include(u => u.User);

    public override async Task<UserFood?> FindByIdAsync(int userFood, CancellationToken cancellationToken = default)
        => await FindAll(uf => uf.Id == userFood).FirstOrDefaultAsync(cancellationToken);

    public async Task<UserFood?> FindByUserId(string userId, CancellationToken cancellationToken = default)
        => await FindAll(uf => uf.User.Id.Equals(userId)).FirstOrDefaultAsync(cancellationToken);
}
