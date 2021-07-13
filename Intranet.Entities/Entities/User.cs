using System;
using System.Collections.Generic;

namespace Intranet.Entities.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? ProfilePic { get; set; } = String.Empty;
        public string? Bio { get; set; } = String.Empty;
        public bool Company { get; set; }
        public string? Age { get; set; }
        public bool? Gender { get; set; }
        public bool IsDisable { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public int? GroupChatId { get; set; }

        public virtual GroupChat? GroupChat { get; set; }
    }
}
