using System;
using System.Collections.Generic;
using System.Text;

namespace Intranet.DataObject
{
    public class TeamDTO : BaseDTO
    {
        public string TeamName { get; set; }
        public string Clients { get; set; }
        public string About { get; set; }
        public bool Company { get; set; }
        public int TechLead { get; set; }
        public ICollection<UserDTO> Members { get; set; } = Array.Empty<UserDTO>();
    }
}
