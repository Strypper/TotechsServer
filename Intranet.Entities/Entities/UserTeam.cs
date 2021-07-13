using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.Entities.Entities
{
    public class UserTeam : BaseEntity
    {
        public int UserId { get; set; }
        public int TeamId { get; set; }
        public virtual User User { get; set; }
        public virtual Team Team { get; set; }
    }
}
