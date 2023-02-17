using Intranet.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Contract;

public interface IFeatureRepository : IRepositoryBase<Feature>
{
    Task DeleteAll(CancellationToken cancelationToken = default);
}
