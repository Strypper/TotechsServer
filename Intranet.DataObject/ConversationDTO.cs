using System.Collections.Generic;

namespace Intranet.DataObject
{
    public class ConversationDTO : BaseDTO
    {
        public ICollection<ChatMessageDTO> ChatMessages { get; set; } = new HashSet<ChatMessageDTO>();
        public ICollection<UserDTO> Users { get; set; } = new HashSet<UserDTO>();
    }
}