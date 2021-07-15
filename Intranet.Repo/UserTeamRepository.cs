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
    public class UserTeamRepository : BaseRepository<UserTeam>, IUserTeamRepository
    {
        public UserTeamRepository(IntranetContext ic) : base(ic)
        {

        }
        public override IQueryable<UserTeam> FindAll(Expression<Func<UserTeam, bool>>? predicate = null)
        => (predicate == null ? _dbSet.Where(x => true) : _dbSet.Where(predicate)).Include(t => t.Team).Include(u => u.User);

        public override async Task<UserTeam> FindByIdAsync(int userTeam, CancellationToken cancellationToken = default)
            => await FindAll(uf => uf.Id == userTeam).FirstOrDefaultAsync(cancellationToken);

        public async Task<UserTeam> FindByUserId(int userId, CancellationToken cancellationToken = default)
            => await FindAll(uf => uf.User.Id == userId).FirstOrDefaultAsync(cancellationToken);

    }
}
