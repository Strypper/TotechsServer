using Intranet.Contract;
using Intranet.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Repo;

public class FeatureRepository : BaseRepository<Feature>, IFeatureRepository
{
    public FeatureRepository(IntranetContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task DeleteAll(CancellationToken cancellationToken = default)
    {
        var features = await FindAll().ToListAsync(cancellationToken);
        _dbSet.RemoveRange(features!);
    }
}
