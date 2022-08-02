using System;
using System.Collections.Generic;

namespace Intranet.DataObject
{
    public class ChatMessageDTO : BaseDTO<int>
    {
        public UserDTO?         User           { get; set; }
        public string ?         MessageContent { get; set; }
        public DateTime         SentTime       { get; set; }
        public ConversationDTO? Conversation   { get; set; }
    }
}