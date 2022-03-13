using System;
using System.Collections.Generic;

namespace Intranet.DataObject
{
    public class ProjectDTO : BaseDTO
    {
        public string ProjectName { get; set; }
        public string? Clients    { get; set; } = String.Empty;
        public string? About      { get; set; } = String.Empty;
        public int     TechLead   { get; set; }
    }

    public class ProjectWithMemberDTO : ProjectDTO
    {
        public IEnumerable<UserDTO> Members { get; set; }
    }
}
