using Intranet.Contract;
using Intranet.Entities.Database;
using Intranet.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Repo
{
    public class ContributionRepository : BaseRepository<Contribution>, IContributionRepository
    {
        public ContributionRepository(IntranetContext ic) : base(ic) { }
        public async Task DeleteAll(CancellationToken cancellationToken = default)
        {
            var contributions = await FindAll().ToListAsync(cancellationToken);
            _dbSet.RemoveRange(contributions);
        }
    }
}
