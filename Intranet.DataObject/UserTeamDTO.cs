using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.DataObject
{

    public class UserTeamDTO : BaseDTO
    {
        public UserDTO User { get; set; }
        public TeamDTO Team { get; set; }
    }

    public class CreateUpdateUserTeamDTO
    {
        public int UserId { get; set; }
        public int TeamId { get; set; }
    }
}
