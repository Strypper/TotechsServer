using System;

namespace Intranet.Entities;

public class ChatMessage : BaseEntity<int>
{
    public string MessageContent { get; set; } = default!;
    public DateTime SentTime { get; set; }

    public int ConversationId { get; set; }

    public string UserId { get; set; } = string.Empty;
    public User User { get; set; } = default!;
}
