﻿namespace Intranet.DataObject;

public class UserConversationDTO : BaseDTO<int>
{
    public UserDTO User { get; set; }
    public ConversationDTO Conversation { get; set; }
}
public class CreateUpdateUserConversationDTO
{
    public string CurrentUserId { get; set; }
    public string TargetUserId { get; set; }
}
