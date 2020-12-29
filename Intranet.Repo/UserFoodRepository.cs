using Intranet.Contract;
using Intranet.Entities.Database;
using Intranet.Entities.Entities;

namespace Intranet.Repo
{
    public class UserFoodRepository : BaseRepository<UserFood>, IUserFoodRepository
    {
        public UserFoodRepository(IntranetContext ic) : base(ic)
        {

        }
    }
}
