using System;

namespace Intranet.Entities;

public class ChatMessage : BaseEntity<int>
{
    public User User { get; set; } = default!;
    public string MessageContent { get; set; } = default!;
    public DateTime SentTime { get; set; }

    public virtual Conversation Conversation { get; set; } = default!;
}
