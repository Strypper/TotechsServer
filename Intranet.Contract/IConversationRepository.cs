using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Contract;

public interface IConversationRepository : IRepositoryBase<Conversation>
{
    Task<Conversation?> FindByNameAsync(string name, CancellationToken cancellationToken = default);
}
