using Intranet.Contract;
using Intranet.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Repo;

public class UserProjectRepository : BaseRepository<UserProject>, IUserProjectRepository
{
    public UserProjectRepository(IntranetContext ic) : base(ic)
    {

    }
    public override IQueryable<UserProject> FindAll(Expression<Func<UserProject, bool>>? predicate = null)
    => (predicate == null ? _dbSet.Where(x => true) : _dbSet.Where(predicate)).Include(t => t.Project).Include(u => u.User);

    public override async Task<UserProject?> FindByIdAsync(int userTeam, CancellationToken cancellationToken = default)
        => await FindAll(uf => uf.Id == userTeam).FirstOrDefaultAsync(cancellationToken);

    public async Task<UserProject?> FindByUserId(string userId, CancellationToken cancellationToken = default)
        => await FindAll(uf => uf.User.Id.Equals(userId)).FirstOrDefaultAsync(cancellationToken);

}
