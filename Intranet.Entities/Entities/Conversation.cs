using System.Collections.Generic;

namespace Intranet.Entities.Entities
{
    public class Conversation : BaseEntity
    {
        public ICollection<ChatMessage> ChatMessages { get; set; } = new HashSet<ChatMessage>();
        public ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
