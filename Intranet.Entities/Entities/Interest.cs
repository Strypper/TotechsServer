using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.Entities.Entities
{
    public class Interest : BaseEntity<int>
    {
        public string Name      { get; set; } = "No Name Hobby";
        public string IconURL   { get; set; } = string.Empty;

        public ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
