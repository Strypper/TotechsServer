using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Hubs;

public class MAUIslandHub : Hub
{
    //private IMapper _mapper;
    //private IUserRepository _userRepository;
    //private IGroupChatRepository _groupChatRepository;
    //private IChatMessageRepository _chatMessageRepository;
    //private IConversationRepository _conversationRepository;

    public MAUIslandHub(//IMapper mapper
                       //IUserRepository userRepository,
                       //IGroupChatRepository groupChatRepository,
                       //IChatMessageRepository chatMessageRepository,
                       //IConversationRepository conversationRepository)
                       )
    {
        //_mapper = mapper;
        //_userRepository = userRepository;
        //_groupChatRepository = groupChatRepository;
        //_chatMessageRepository = chatMessageRepository;
        //_conversationRepository = conversationRepository;
    }

    public override async Task OnConnectedAsync()
    {
        System.Diagnostics.Debug.WriteLine(Context.ConnectionId);
        //var allOnlineUsers = _userRepository
        //                            .FindAll(user => user.SignalRConnectionId != null)
        //                            .ToList();
        
        await Clients.All.SendAsync("ReceiveMessage", $"Welcome {Context.ConnectionId}");
        await base.OnConnectedAsync();
    }

    //public override async Task OnDisconnectedAsync(Exception exception)
    //{
    //    await base.OnDisconnectedAsync(exception);
    //}

    public async Task SendMessage(string mess)
    {
        CancellationToken cancellationToken = new CancellationToken(default);
        await Clients.All.SendAsync("ReceiveMessage", mess);
        await Clients.Caller.SendAsync("ReceiveMessage", mess);
    }
}
