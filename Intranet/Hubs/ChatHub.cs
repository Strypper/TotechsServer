using Intranet.Entities.Database;
using Intranet.Entities.Entities;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Hubs
{
    public class ChatHub : Hub
    {
        private IntranetContext _ic;
        public ChatHub(IntranetContext ic)
        {
            _ic = ic;
        }
        public async Task SendMessage(string mess, CancellationToken cancellationToken = default)
        {
            string userId = Context.User.FindFirst(c => c.Type == "UserId").Value;
            var user = await _ic.FindAsync<User>(new object[] { userId }, cancellationToken);
            await Clients.All.SendAsync("HubMessage", user.UserName, mess, cancellationToken);
        }
    }
}
