using System.Collections.Generic;

namespace Intranet.Entities.Entities
{
    public class Team : BaseEntity
    {
        public string TeamName { get; set; }
        public string Clients { get; set; }
        public string About { get; set; }
        public bool Company { get; set; }
        public int TechLead { get; set; }
        public ICollection<User> Members { get; set; } = new HashSet<User>();
    }
}
