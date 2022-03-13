using System.Collections.Generic;

namespace Intranet.DataObject
{

    public class UserProjectDTO : BaseDTO
    {
        public UserDTO    User    { get; set; }
        public ProjectDTO Project { get; set; }
    }

    public class CreateUpdateUserProjectDTO
    {
        public int UserId    { get; set; }
        public int ProjectId { get; set; }
    }

    public class CreateProjectWithMultipleUsers : BaseDTO
    {
        public ICollection<UserDTO> Members     { get; set; } = new HashSet<UserDTO>();
        public string               ProjectName { get; set; }
        public string               Clients     { get; set; }
        public string               About       { get; set; }
        public int                  TechLead    { get; set; }
    }
}
