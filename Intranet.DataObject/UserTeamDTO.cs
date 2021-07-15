using System.Collections.Generic;

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

    public class CreateTeamWithMultipleUsers
    {
        public ICollection<UserDTO> Users { get; set; } = new HashSet<UserDTO>();
        public TeamDTO Team { get; set; }
    }
}
