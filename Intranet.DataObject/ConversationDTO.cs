using System.Collections.Generic;

namespace Intranet.DataObject
{
    public class ConversationDTO : BaseDTO
    {
        public ICollection<ChatMessageDTO> ChatMessages { get; set; } = new HashSet<ChatMessageDTO>();
        public ICollection<UserDTO> Users { get; set; } = new HashSet<UserDTO>();
    }

    public class CreateConversationDTO
    {
        public string MessageContent { get; set; }
        public int CurrentUserId { get; set; }
        public int TargerUserId { get; set; }
    }
}