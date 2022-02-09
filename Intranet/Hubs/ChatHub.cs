using AutoMapper;
using Intranet.Contract;
using Intranet.DataObject;
using Intranet.Entities.Entities;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Hubs
{
    public class ChatHub : Hub
    {
        private IMapper _mapper;
        private IUserRepository _userRepository;
        private IGroupChatRepository _groupChatRepository;
        private IChatMessageRepository _chatMessageRepository;
        private IConversationRepository _conversationRepository;

        public ChatHub(IMapper mapper, 
                       IUserRepository userRepository, 
                       IGroupChatRepository groupChatRepository, 
                       IChatMessageRepository chatMessageRepository,
                       IConversationRepository conversationRepository)
        {
            _mapper                 = mapper;
            _userRepository         = userRepository;
            _groupChatRepository    = groupChatRepository;
            _chatMessageRepository  = chatMessageRepository;
            _conversationRepository = conversationRepository;
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

            var user = await _userRepository.FindBySignalRConnectionId(Context.ConnectionId);
            if (user != null)
            {
                user.SignalRConnectionId = null;
                _userRepository.Update(user);
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task JoinGroup(int groupId, int userId)
        {
            CancellationToken token = new CancellationToken(default);
            var user  = await _userRepository.FindByIdAsync(userId, token);
            var group = await _groupChatRepository.FindByIdAsync(groupId, token);
            await Groups.AddToGroupAsync(Context.ConnectionId , group.GroupName);
        }

        public async Task IdentifyUser(string connectionId, int userId)
        {
            CancellationToken token = new CancellationToken(default);
            var user = await _userRepository.FindByIdAsync(userId, token);

            user.SignalRConnectionId = connectionId;
            _userRepository.Update(user);

            await Clients.Client(connectionId).SendAsync("ChatHubUserIndentity", _mapper.Map<UserDTO>(user));
        }

        public async Task SendMessage(string mess, 
                                      int conversationId, 
                                      int fromUserId, 
                                      int toUserId)
        {
            CancellationToken cancellationToken = new CancellationToken(default);
            var conversation = await _conversationRepository.FindByIdAsync(conversationId, cancellationToken);
            if(conversation != null){
                var fromUser     = await _userRepository.FindByIdAsync(fromUserId, cancellationToken);
                var toUser       = await _userRepository.FindByIdAsync(toUserId, cancellationToken);
                _chatMessageRepository.Create(new ChatMessage(){
                    User           = fromUser,
                    MessageContent = mess,
                    SentTime       = DateTime.UtcNow,
                    Conversation   = conversation
                });
                conversation.LastMessageContent = mess;
                conversation.LastInteractionTime = DateTime.UtcNow;
                _conversationRepository.Update(conversation);
                await _chatMessageRepository.SaveChangesAsync(cancellationToken);
                await _conversationRepository.SaveChangesAsync(cancellationToken);
                if (toUser.SignalRConnectionId != null) await Clients.Client(toUser.SignalRConnectionId).SendAsync("ReceiveMessage", mess, _mapper.Map<UserDTO>(fromUser));
            }
            else{
                System.Diagnostics.Debug.WriteLine("The conversation does not exist");
            }
        }
    }
}
