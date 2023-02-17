using Intranet.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Contract;

public interface IProjectTaskRepository : IRepositoryBase<ProjectTask>
{
    Task DeleteAll(CancellationToken cancelationToken = default);
}
