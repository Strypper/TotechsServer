namespace Intranet.DataObject;

public class UserConversationDTO : BaseDTO<int>
{
    public UserDTO User { get; set; } = default!;
    public ConversationDTO Conversation { get; set; } = default!;
}
public class CreateUpdateUserConversationDTO
{
    public string CurrentUserId { get; set; } = default!;
    public string TargetUserId { get; set; } = default!;
}
