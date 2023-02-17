using Intranet.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Contract;

public interface IMediaAssetsRepository : IRepositoryBase<MediaAssets>
{
    Task DeleteAll(CancellationToken cancellationToken);
}
