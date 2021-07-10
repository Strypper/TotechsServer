using System;
using System.Collections.Generic;
using System.Text;

namespace Intranet.DataObject
{
    public class UserDTO : BaseDTO
    {
        public string UserName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? ProfilePic { get; set; }
        public string? Bio { get; set; }
        public bool? Company { get; set; }
        public string? Age { get; set; }
        public bool IsDisable { get; set; }
        public string Role { get; set; }
        public bool? Gender { get; set; }
        public string Password { get; set; }
    }

    public class UserLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
