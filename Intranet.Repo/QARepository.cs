using Intranet.Contract;
using Intranet.Entities;

namespace Intranet.Repo;

public class QARepository : BaseRepository<QA>, IQARepository
{
    public QARepository(IntranetContext repositoryContext) : base(repositoryContext)
    {
    }
}
