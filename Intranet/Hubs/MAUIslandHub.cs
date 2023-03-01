using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;

namespace Intranet.Hubs;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class MAUIslandHub : Hub
{
    #region [Services]
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IConversationRepository _conversationRepository;
    #endregion

    #region [CTor]

    public MAUIslandHub(
                        IMapper mapper,
                        IUserRepository userRepository,
                        IConversationRepository conversationRepository
                       )
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _conversationRepository = conversationRepository;
    }

    #endregion

    #region [Overrides]

    public override async Task OnConnectedAsync()
    {
        CancellationToken cancellationToken = new CancellationToken(default);

        var guid = Context.User?.Claims?.First(c => c.Type == "guid")?.Value;
        var loginUser = await _userRepository.FindByGuidAsync(guid!, cancellationToken);
        if (loginUser is not null)
        {
            loginUser!.SignalRConnectionId = Context.ConnectionId;
            await _userRepository.UpdateUser(loginUser, cancellationToken);
            await Clients.All.SendAsync("MAUIslandHub", $"Welcome {loginUser!.UserName}");
        }

        await Clients.All.SendAsync("MAUIslandHub", $"Welcome Tourist");

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        CancellationToken cancellationToken = new CancellationToken(default);

        var connectionId = Context.ConnectionId;
        var logoffUser = await _userRepository.FindBySignalRConnectionId(connectionId, cancellationToken);
        logoffUser!.SignalRConnectionId = string.Empty;
        await _userRepository.UpdateUser(logoffUser, cancellationToken);


        await Clients.All.SendAsync("MAUIslandHub", $"{logoffUser.UserName} Logoff");
        await base.OnDisconnectedAsync(exception);
    }
    #endregion

    #region [Channels]
    public async Task SendMessage(string message)
    {
        CancellationToken cancellationToken = new CancellationToken(default);

        var signalRConnectionId = Context.ConnectionId;

        var userInfo = await _userRepository.FindBySignalRConnectionId(signalRConnectionId, cancellationToken);

        var mauislandLobby = await _conversationRepository.FindByNameAsync("MAUIslandLobby", cancellationToken);

        var chatMessage = new ChatMessage()
        {
            User = userInfo!,
            MessageContent = message,
            SentTime = DateTime.UtcNow,
        };

        mauislandLobby!.ChatMessages.Add(chatMessage);

        await _conversationRepository.SaveChangesAsync(cancellationToken);

        await Clients.All.SendAsync("ReceiveMessage",
                                    message,
                                    userInfo!.UserName,
                                    userInfo.ProfilePic ?? "https://i.imgur.com/deS4147.png",
                                    DateTime.Now);
    }
    #endregion

}
