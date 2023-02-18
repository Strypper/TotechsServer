using Intranet.Contract;
using Intranet.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Repo;

public class ContributionRepository : BaseRepository<Contribution>, IContributionRepository
{
    public ContributionRepository(IntranetContext ic) : base(ic) { }
    public override Task<Contribution?> FindByIdAsync(int id, CancellationToken cancellationToken)
    {
        return base.FindByIdAsync(id, cancellationToken);
    }
    public async Task DeleteAll(CancellationToken cancellationToken = default)
    {
        var contributions = await FindAll().ToListAsync(cancellationToken);
        _dbSet.RemoveRange(contributions!);
    }
}
