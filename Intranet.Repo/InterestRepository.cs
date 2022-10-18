using Intranet.Contract;
using Intranet.Entities.Database;
using Intranet.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Repo
{
    public class InterestRepository : BaseRepository<Interest>, IInterestRepository
    {
        public InterestRepository(IntranetContext ic) : base(ic) { }

        public async Task DeleteAll(CancellationToken cancellationToken = default)
        {
            var interests = await FindAll().ToListAsync(cancellationToken);
            _dbSet.RemoveRange(interests);
        }
    }
}
