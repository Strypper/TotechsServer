using System;

namespace Intranet.Entities.Entities
{
    public class User : BaseEntity<int>
    {
        public string Guid { get; set; }
        public string UserName { get; set; }
        public string? ProfilePic { get; set; } = String.Empty;
        public string? CardPic { get; set; } = String.Empty;
        public string? Bio { get; set; } = String.Empty;
        public string? Former { get; set; }
        public string? Hobby { get; set; }
        public string SpecialAward { get; set; } = "No Award Yet";
        public bool IsDisable { get; set; }
        public string? Skills { get; set; }
        public string? Relationship { get; set; }
        public string? SignalRConnectionId { get; set; }

    }
}
