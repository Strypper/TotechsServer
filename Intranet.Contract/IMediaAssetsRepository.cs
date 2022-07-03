using Intranet.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Contract
{
    public interface IMediaAssetsRepository : IRepositoryBase<MediaAssets>
    {
        Task DeleteAll(CancellationToken cancellationToken);
    }
}
