using Intranet.Contract;
using Intranet.Entities;

namespace Intranet.Repo;

public class UserQARepository : BaseRepository<UserQA>, IUserQARepository
{
    public UserQARepository(IntranetContext ic) : base(ic) { }
}
