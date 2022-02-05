﻿using System;
using System.Collections.Generic;

namespace Intranet.Entities.Entities
{
    public class Conversation : BaseEntity
    {
        public ICollection<ChatMessage> ChatMessages        { get; set; } = new HashSet<ChatMessage>();
        public ICollection<User>        Users               { get; set; } = new HashSet<User>();
        public DateTime                 DateCreated         { get; set; } = DateTime.UtcNow;
        public DateTime                 LastInteractionTime { get; set; }
        public string                   LastMessageContent  { get; set; } = String.Empty;
    }
}
