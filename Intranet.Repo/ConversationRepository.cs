using Intranet.Contract;
using Intranet.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Repo;

public class ConversationRepository : BaseRepository<Conversation>, IConversationRepository
{
    public ConversationRepository(IntranetContext ic) : base(ic) { }

    public override async Task<Conversation?> FindByIdAsync(int id, CancellationToken cancellationToken)
        => await FindAll(conversaion => conversaion!.Id == id)
                .Include(conversation => conversation!.ChatMessages)
                    .Take(10)
                .FirstOrDefaultAsync(cancellationToken);

    public async Task<Conversation?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
        => await _dbSet.Include(conversation => conversation!.ChatMessages)
                            .ThenInclude(chatMessage => chatMessage.User)
                        .Take(10).FirstOrDefaultAsync(x => x.Name == name!, cancellationToken);

    //public async Task<Conversation> FindConversationAsync(List<User> users)
    //{
    //    var query        = _dbSet.AsQueryable()
    //                             .AsNoTracking();
    //    var conversationContainUserA = await query.FirstOrDefaultAsync(_conversationContainUserA => _conversationContainUserA.Users.Contains(users[0]));
    //    if(conversationContainUserA != null)
    //    {

    //    }
    //    var conversation = await query.FirstOrDefaultAsync(_conversation => _conversation.Users.ToList().Find(user => user.));
    //}
}
