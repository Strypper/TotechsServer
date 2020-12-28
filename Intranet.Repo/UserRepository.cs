using Intranet.Contract;
using Intranet.Entities.Database;
using Intranet.Entities.Entities;

namespace Intranet.Repo
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IntranetContext ic): base(ic){}
    }
}
