using Intranet.Contract;
using Intranet.Entities;
using Intranet.Entities.Entities;

namespace Intranet.Repo
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(IntranetContext ic) : base(ic) { }
    }
}
