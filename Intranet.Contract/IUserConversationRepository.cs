﻿using Intranet.Entities.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Contract
{
    public interface IUserConversationRepository : IRepositoryBase<UserConversation>
    {
        Task<IEnumerable<UserConversation>> FindByUserId(int userId, CancellationToken cancellationToken);
        Task<IEnumerable<UserConversation>> FindByConversationId(int conversationId, CancellationToken cancellationToken);
    }
}
