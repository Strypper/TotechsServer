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

    public class CreateTeamWithMultipleUsers : BaseDTO
    {
        public ICollection<UserDTO> Members { get; set; } = new HashSet<UserDTO>();
        public string TeamName { get; set; }
        public string Clients { get; set; }
        public string About { get; set; }
        public bool Company { get; set; }
        public int TechLead { get; set; }
    }
}
