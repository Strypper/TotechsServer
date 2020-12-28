using System;
using System.Collections.Generic;
using System.Text;

namespace Intranet.DataObject
{
    public class UserDTO : BaseDTO
    {
        public string UserName { get; set; }
        public bool Company { get; set; }
        public string Age { get; set; }
        public bool Gender { get; set; }
        public string Password { get; set; }
    }
}
