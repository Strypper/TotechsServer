using Intranet.Contract;
using Intranet.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Repo;

public class ProjectTaskRepository : BaseRepository<ProjectTask>, IProjectTaskRepository
{
    public ProjectTaskRepository(IntranetContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task DeleteAll(CancellationToken cancellationToken = default)
    {
        var projectTasks = await FindAll().ToListAsync(cancellationToken);
        _dbSet.RemoveRange(projectTasks!);
    }
}
