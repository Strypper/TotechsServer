using System;
using System.Collections.Generic;

namespace Intranet.Entities.Entities
{
    public class GroupChat : BaseEntity
    {
        public string GroupName { get; set; }
        public string GroupImage { get; set; }
        public string GroupColor { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
        public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new HashSet<ChatMessage>();
    }
}
