using System;
using System.Collections.Generic;
using System.Text;

namespace Intranet.Entities.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string ProfilePic { get; set; } = String.Empty;
        public bool Company { get; set; }
        public string Age { get; set; }
        public bool Gender { get; set; }
        public string Password { get; set; }
    }
}
