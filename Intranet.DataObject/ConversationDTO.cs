using System;
using System.Collections.Generic;

namespace Intranet.DataObject;

public class ConversationDTO : BaseDTO<int>
{
    public ICollection<ChatMessageDTO> ChatMessages { get; set; } = new HashSet<ChatMessageDTO>();
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public DateTime LastInteractionTime { get; set; }
    public string LastMessageContent { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}

public class CreateConversationDTO
{
    public string? MessageContent { get; set; }
    public string CurrentUserGuid { get; set; } = String.Empty;
    public string TargerUserGuid { get; set; } = String.Empty;
}

public class ConversationDirectModeDTO : ConversationDTO
{
    public ICollection<UserDTO> Users { get; set; } = new HashSet<UserDTO>();
}