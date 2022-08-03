using System;

namespace Intranet.Entities.Entities
{
    public class ChatMessage : BaseEntity<int>
    {
        public User User { get; set; }
        public string MessageContent { get; set; }
        public DateTime SentTime { get; set; }

        public virtual Conversation Conversation { get; set; }
    }
}
