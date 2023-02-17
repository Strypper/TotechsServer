using Intranet.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Contract;

public interface IContributionRepository : IRepositoryBase<Contribution>
{
    Task DeleteAll(CancellationToken cancelationToken = default);
}
