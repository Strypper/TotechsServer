using System;
using System.Collections.Generic;
using System.Text;

namespace Intranet.DataObject
{
    public class UserDTO : BaseDTO
    {
        public string UserName { get; set; }
        public string? Country { get; set; }
        public string? Former { get; set; }
        public string? Hobby { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? ProfilePic { get; set; }
        public string? Bio { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? Company { get; set; }
        public bool IsDisable { get; set; }
        public string? Role { get; set; }
        public string? Level { get; set; }
        public bool? Gender { get; set; }
        public string Password { get; set; }
        public string? SpecialAward { get; set; }
        public int? Like { get; set; }
        public int? Friendly { get; set; }
        public int? Funny { get; set; }
        public int? Enthusiastic { get; set; }
        public string? Relationship { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? SignalRConnectionId { get; set; }

        public ICollection<SkillDTO>? Skills { get; set; } = Array.Empty<SkillDTO>();
    }

    public class UserLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
