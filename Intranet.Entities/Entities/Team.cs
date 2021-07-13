using System.Collections.Generic;

namespace Intranet.Entities.Entities
{
    public class Team : BaseEntity
    {
        public string TeamName { get; set; }
        public string? Clients { get; set; } = string.Empty;
        public string? About { get; set; } = string.Empty;
        public bool Company { get; set; }
        public int TechLead { get; set; }
    }
}
