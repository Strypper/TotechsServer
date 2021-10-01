using System;
using System.Collections.Generic;
using System.Linq;

namespace Intranet.Entities.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string? Country { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? ProfilePic { get; set; } = String.Empty;
        public string? Bio { get; set; } = String.Empty;
        public string? PhoneNumber { get; set; }
        public string? Former { get; set; }
        public string? Hobby { get; set; }
        public string SpecialAward { get; set; } = "No Award Yet";
        public bool Company { get; set; }
        public bool? Gender { get; set; }
        public bool IsDisable { get; set; }
        public string? Role { get; set; }
        public string? Level { get; set; }
        public string Password { get; set; }
        public int? GroupChatId { get; set; }
        public int? Like { get; set; }
        public int? Friendly { get; set; }
        public int? Funny { get; set; }
        public int? Enthusiastic { get; set; }
        public string? Skills { get; set; }
        public string? Relationship { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public virtual GroupChat? GroupChat { get; set; }
    }
}
