using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.DataObject
{
    public class UserConversationDTO : BaseDTO
    {
        public UserDTO User { get; set; }
        public ConversationDTO Conversation { get; set; }
    }
    public class CreateUpdateUserConversationDTO
    {
        public int UserId { get; set; }
        public int ConversationId { get; set; }
    }
}
