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
        private IGroupChatRepository _groupChatRepository;
        private IChatMessageRepository _chatMessageRepository;

        public ChatHub(IUserRepository userRepository, IGroupChatRepository groupChatRepository, IChatMessageRepository chatMessageRepository)
        {
            _userRepository = userRepository;
            _groupChatRepository = groupChatRepository;
            _chatMessageRepository = chatMessageRepository;
        }

        public override async Task OnConnectedAsync()
        {
            System.Diagnostics.Debug.WriteLine(Context.ConnectionId);
            await Clients.Caller.SendAsync($"Welcome {Context.ConnectionId}");
            await Clients.All.SendAsync("ReceiveMessage", $"Welcome {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            System.Diagnostics.Debug.WriteLine(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task JoinGroup(int groupId, int userId)
        {
            CancellationToken token = new CancellationToken(default);
            var user = await _userRepository.FindByIdAsync(userId, token);
            var group = await _groupChatRepository.FindByIdAsync(groupId, token);
            await Groups.AddToGroupAsync(Context.ConnectionId , group.GroupName);
        }

        public async Task SendMessage(string mess, int userId)
        {
            CancellationToken token = new CancellationToken(default);
            var user = await _userRepository.FindByIdAsync(userId, token);
            System.Diagnostics.Debug.WriteLine(mess);
            await Clients.All.SendAsync("ReceiveMessage", mess, user);
        }
    }
}
