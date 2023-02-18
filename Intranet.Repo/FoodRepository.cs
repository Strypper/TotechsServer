using Intranet.Contract;
using Intranet.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Repo;

public class FoodRepository : BaseRepository<Food>, IFoodRepository
{
    public FoodRepository(IntranetContext ic) : base(ic) { }

    public async Task DeleteAll(CancellationToken cancellationToken = default)
    {
        var foods = await FindAll().ToListAsync(cancellationToken);
        _dbSet.RemoveRange(foods!);
    }
}
