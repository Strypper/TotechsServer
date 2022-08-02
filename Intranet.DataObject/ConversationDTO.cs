using System;
using System.Collections.Generic;

namespace Intranet.DataObject
{
    public class ConversationDTO : BaseDTO<int>
    {
        public ICollection<ChatMessageDTO> ChatMessages        { get; set; } = new HashSet<ChatMessageDTO>();
        public DateTime                    DateCreated         { get; set; } = DateTime.UtcNow;
        public DateTime                    LastInteractionTime { get; set; }
        public string                      LastMessageContent  { get; set; } = String.Empty;
    }

    public class CreateConversationDTO
    {
        public string MessageContent { get; set; }
        public int    CurrentUserId { get; set; }
        public int    TargerUserId { get; set; }
    }

    public class ConversationDirectModeDTO : ConversationDTO
    {
        public ICollection<UserDTO> Users { get; set; } = new HashSet<UserDTO>();
    }
}