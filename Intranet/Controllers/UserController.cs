using Microsoft.AspNetCore.Http;

namespace Intranet;

[Authorize(AuthenticationSchemes = "Bearer")]
[Route("/api/[controller]/[action]")]
public class UserController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IMediaService _mediaService;
    private readonly IUserRepository _userRepository;
    private readonly IntranetContext _intranetContext;
    private readonly IJWTTokenService _jwtTokenService;
    private readonly IChatMessageRepository _chatMessageRepository;
    private readonly IConversationRepository _conversationRepository;
    private readonly IUserConversationRepository _userConversationRepository;

    public UserController(IMapper mapper,
                          IMediaService mediaService,
                          IUserRepository userRepository,
                          IntranetContext intranetContext,
                          IJWTTokenService jwtTokenService,
                          IChatMessageRepository chatMessageRepository,
                          IConversationRepository conversationRepository,
                          IUserConversationRepository userConversationRepository)
    {
        _mapper = mapper;
        _mediaService = mediaService;
        _userRepository = userRepository;
        _intranetContext = intranetContext;
        _jwtTokenService = jwtTokenService;
        _chatMessageRepository = chatMessageRepository;
        _conversationRepository = conversationRepository;
        _userConversationRepository = userConversationRepository;
    }

    #region [GET]

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var users = await _userRepository.FindAll().ToListAsync(cancellationToken);
        return Ok(_mapper.Map<IEnumerable<UserDTO>>(users));
    }

    [HttpGet]
    public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
    {
        var guid = HttpContext.User.FindFirst("guid")?.Value;
        if (guid is null)
            return BadRequest("We could not find user guid in your request");
        var user = await _userRepository.FindByGuidAsync(guid, cancellationToken);
        if (user is null)
            return BadRequest("We couldn't find this user in our database");
        return Ok(_mapper.Map<UserDTO>(user));
    }

    [HttpGet("{singalRConnectionStringId}")]
    public async Task<IActionResult> GetUserBySingalRConnectionStringId(string singalRConnectionStringId, CancellationToken cancellationToken = default)
    {
        if (!string.IsNullOrEmpty(singalRConnectionStringId) && !string.IsNullOrWhiteSpace(singalRConnectionStringId))
        {
            var user = await _userRepository.FindBySignalRConnectionId(singalRConnectionStringId);
            return Ok(_mapper.Map<UserDTO>(user));
        }
        else return NotFound();
    }

    [HttpGet("{guid}")]
    public async Task<IActionResult> GetUserByGuid(string guid, CancellationToken cancellationToken = default)
    {
        if (!string.IsNullOrEmpty(guid) && !string.IsNullOrWhiteSpace(guid))
        {
            var user = await _userRepository.FindByGuidAsync(guid, cancellationToken);
            return Ok(_mapper.Map<UserDTO>(user));
        }
        else return NotFound();
    }

    #endregion

    #region [PUT]

    [HttpPut]
    public async Task<IActionResult> Update(UserDTO dto, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.FindByGuidAsync(dto.Guid, cancellationToken);
        if (user is null) return NotFound();
        _mapper.Map(dto, user);
        await _userRepository.UpdateUser(user, cancellationToken);
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAvatar(IFormFile avatar, CancellationToken cancellationToken = default)
    {
        var guid = HttpContext.User.FindFirst("guid")?.Value;
        if (guid is null)
            return BadRequest("We could not find user guid in your request");
        var user = await _userRepository.FindByGuidAsync(guid, cancellationToken);
        if (user is null)
            return BadRequest("We couldn't find this user in our database");
        if (_mediaService.IsImage(avatar))
        {
            using (Stream stream = avatar.OpenReadStream())
            {
                Tuple<bool, string> uploadresults = await _mediaService.UploadAvatarToStorage(stream, avatar.FileName);
                var isUploaded = uploadresults.Item1;
                var stringUrl = uploadresults.Item2;
                if (isUploaded && !string.IsNullOrEmpty(stringUrl))
                {
                    user.ProfilePic = stringUrl;
                    await _userRepository.UpdateUser(user, cancellationToken);
                    return Ok();
                }
                else return BadRequest("Look like the image couldnt upload to the storage, but your account have created successfully");
            }
        }
        return new UnsupportedMediaTypeResult();
    }
    #endregion

    #region [DELETE]

    [HttpDelete("{guid}")]
    public async Task<IActionResult> DeleteByGuid(string guid, CancellationToken cancellationToken)
    {
        var userInfo = await _userRepository.FindByGuidAsync($"{guid}", cancellationToken);
        if (userInfo is null) return NotFound();
        await _userRepository.DeleteUser(userInfo);
        return NoContent();
    }

    #endregion
}