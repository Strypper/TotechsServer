using System;
using System.Collections.Generic;
using System.Text;

namespace Intranet.DataObject
{
    public class UserDTO : BaseDTO
    {
        public string  UserName { get; set; }
        public string? Bio   { get; set; }
        public string? Former { get; set; }
        public string? Hobby { get; set; }
        public string? CardPic { get; set; } 
        public bool    IsDisable { get; set; }
        public string? SpecialAward { get; set; }
        public int?    Like { get; set; }
        public int?    Friendly { get; set; }
        public int?    Funny { get; set; }
        public int?    Enthusiastic { get; set; }
        public string? Relationship { get; set; }
        public string? SignalRConnectionId { get; set; }

        public ICollection<SkillDTO>? Skills { get; set; } = Array.Empty<SkillDTO>();
    }

    public class UserLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
