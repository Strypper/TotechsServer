using Intranet.Contract;
using Intranet.Entities.Database;
using Intranet.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Intranet.Repo
{
    public class UserFoodRepository : BaseRepository<UserFood>, IUserFoodRepository
    {
        public UserFoodRepository(IntranetContext ic) : base(ic)
        {

        }

        public async Task<UserFood> FindByUserId(int userId)
            => await FindAll(uf => uf.User.Id == userId).FirstOrDefaultAsync();
    }
}
