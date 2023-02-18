using Intranet.Contract;
using Intranet.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Repo;

public class MediaAssetsRepository : BaseRepository<MediaAssets>, IMediaAssetsRepository
{
    public MediaAssetsRepository(IntranetContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task DeleteAll(CancellationToken cancellationToken = default)
    {
        var mediaAssets = await FindAll().ToListAsync(cancellationToken);
        _dbSet.RemoveRange(mediaAssets!);
    }
}
