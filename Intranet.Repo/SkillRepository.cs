using Intranet.Contract;
using Intranet.Entities.Database;
using Intranet.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Repo
{
    public class SkillRepository : BaseRepository<Skill>, ISkillRepository
    {
        public SkillRepository(IntranetContext ic) : base(ic) { }

        public async Task DeleteAll(CancellationToken cancellationToken = default)
        {
            var skills = await FindAll().ToListAsync(cancellationToken);
            _dbSet.RemoveRange(skills);
        }
    }
}
