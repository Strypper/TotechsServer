using Intranet.Contract;
using Intranet.Entities.Database;
using Intranet.Entities.Entities;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Hubs
{
    public class ChatHub : Hub
    {
        private IUserRepository _userRepository;
        public ChatHub(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public override Task OnConnectedAsync()
        {
            System.Diagnostics.Debug.WriteLine(Context.ConnectionId);
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            System.Diagnostics.Debug.WriteLine(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task IdentifyUser(int userId)
        {
            CancellationToken token = new CancellationToken(default);
            var user = await _userRepository.FindByIdAsync(userId, token);
            System.Diagnostics.Debug.WriteLine(user.UserName);
            await Clients.Caller.SendAsync("UserResult", user);
        }

        public async Task SendMessage(string mess, int userId)
        {
            CancellationToken token = new CancellationToken(default);
            var user = await _userRepository.FindByIdAsync(userId, token);
            System.Diagnostics.Debug.WriteLine(mess);
            await Clients.All.SendAsync("ReceiveMessage", mess, user.UserName);
        }
    }
}
