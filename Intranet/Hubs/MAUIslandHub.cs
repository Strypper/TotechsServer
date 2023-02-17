using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;

namespace Intranet.Hubs;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class MAUIslandHub : Hub
{
    private IMapper _mapper;
    private IUserRepository _userRepository;

    public MAUIslandHub(
                        IMapper mapper,
                        IUserRepository userRepository
                       )
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("ReceiveMessage", $"Welcome {Context.ConnectionId}");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await base.OnDisconnectedAsync(exception);
    }

    public async Task IdentifyUser(string connectionId, int userId)
    {
        CancellationToken cancellationToken = new CancellationToken(default);
        var user = await _userRepository.FindByIdAsync(userId, cancellationToken);
        user.SignalRConnectionId = connectionId;
        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync(cancellationToken);
        await Clients.Client(connectionId).SendAsync("ChatHubUserIndentity",
                                                     _mapper.Map<UserDTO>(user));
        await Clients.All.SendAsync("UserLogIn", _mapper.Map<UserDTO>(user));
    }

    public async Task SendMessage(string message)
    {
        var guid = Context.User?.Claims?.First(c => c.Type == "guid")?.Value;
        if (guid is null)
            return;
        var userInfo = await _userRepository.FindByGuidAsync(guid);

        await Clients.All.SendAsync("ReceiveMessage", message, userInfo.UserName, userInfo.ProfilePic, DateTime.Now);
    }
}
