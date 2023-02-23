using System;
using System.Collections.Generic;

namespace Intranet.Entities;

public class Conversation : BaseEntity<int>
{
    public ICollection<ChatMessage> ChatMessages { get; set; } = new HashSet<ChatMessage>();
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public DateTime LastInteractionTime { get; set; }
    public string LastMessageContent { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
