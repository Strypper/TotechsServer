using System;
using System.Collections.Generic;
using System.Text;

namespace Intranet.Entities.Entities
{
    public class UserFood : BaseEntity
    {
        public User User { get; set; }
        public Food Food { get; set; }
    }
}
