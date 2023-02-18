using System;

namespace Intranet.Entities;

public class UserConversation : BaseEntity<int>
{
    public int ConversationId { get; set; }
    public virtual Conversation Conversation { get; set; } = default!;

    public string UserId { get; set; } = default!;
    public virtual User User { get; set; } = default!;

    public bool IsMute { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    //conversation.ChatMessages.First();
}
