using Intranet.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Contract;

public interface ISubFeatureRepository : IRepositoryBase<SubFeature>
{
    Task DeleteAll(CancellationToken cancelationToken = default);
}
