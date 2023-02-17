using Intranet.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Contract;

public interface IFoodRepository : IRepositoryBase<Food>
{
    Task DeleteAll(CancellationToken cancelationToken = default);
}
