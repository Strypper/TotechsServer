using System;
using System.Collections.Generic;

namespace Intranet.Entities;

public class GroupChat : BaseEntity<int>
{
    public string GroupName { get; set; } = default!;
    public string GroupImage { get; set; } = default!;
    public string GroupColor { get; set; } = default!;
    public DateTime CreatedDate { get; set; }

    public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
    public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new HashSet<ChatMessage>();
}
