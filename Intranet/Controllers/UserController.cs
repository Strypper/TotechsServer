using Microsoft.AspNetCore.Http;

namespace Intranet;

[Authorize(AuthenticationSchemes = "Bearer")]
[Route("/api/[controller]/[action]")]
public class UserController : BaseController
{
    private IMapper _mapper;

    //private IMediaService               _mediaService;
    private IntranetContext _intranetContext;

    private IJWTTokenService _jwtTokenService;
    private IUserRepository _userRepository;
    private IChatMessageRepository _chatMessageRepository;
    private IConversationRepository _conversationRepository;
    private IUserConversationRepository _userConversationRepository;

    public UserController(IMapper mapper,
                          //IMediaService               mediaService,
                          IntranetContext intranetContext,
                          IJWTTokenService jwtTokenService,
                          IUserRepository userRepository,
                          IChatMessageRepository chatMessageRepository,
                          IConversationRepository conversationRepository,
                          IUserConversationRepository userConversationRepository)
    {
        _mapper = mapper;
        //_mediaService               = mediaService;
        _intranetContext = intranetContext;
        _jwtTokenService = jwtTokenService;
        _userRepository = userRepository;
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

    #endregion [GET]

    #region [POST]

    #endregion [POST]

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
    [AllowAnonymous]
    public async Task<IActionResult> TestUpload1(IFormFile file, CancellationToken cancellationToken = default)
    {
        return Ok();
    }

    [HttpPut]
    [AllowAnonymous]
    public async Task<IActionResult> TestUpload2([FromForm] TestUploadFileDTO2 dto, CancellationToken cancellationToken = default)
    {
        return Ok();
    }

    [HttpPut]
    [AllowAnonymous]
    public async Task<IActionResult> TestUpload3([FromForm] TestUploadFileDTO3 dto, CancellationToken cancellationToken = default)
    {
        return Ok();
    }

    //[HttpPost]
    //public async Task<IActionResult> UploadAvatar(string id, IFormFile avatar, CancellationToken cancellationToken = default)
    //{
    //    if (_mediaService.IsImage(avatar))
    //    {
    //        var user = await _userRepository.FindByIdAsync(id, cancellationToken);
    //        if (user is null)
    //        {
    //            return NotFound($"No User With Id {id} Found!");
    //        }
    //        using (Stream stream = avatar.OpenReadStream())
    //        {
    //            Tuple<bool, string> result = await _mediaService.UploadAvatarToStorage(stream, avatar.FileName);
    //            var isUploaded = result.Item1;
    //            var stringUrl = result.Item2;
    //            if (isUploaded && !string.IsNullOrEmpty(stringUrl))
    //            {
    //                user.ProfilePic = stringUrl;
    //                await _userRepository.SaveChangesAsync(cancellationToken);

    //                return Ok(stringUrl);
    //            }
    //            else return BadRequest("Look like the image couldnt upload to the storage");
    //        }
    //    }
    //    return new UnsupportedMediaTypeResult();
    //}

    #region [DELETE]

    [HttpDelete("{guid}")]
    public async Task<IActionResult> DeleteByGuid(string guid, CancellationToken cancellationToken)
    {
        var userInfo = await _userRepository.FindByGuidAsync($"{guid}", cancellationToken);
        if (userInfo is null) return NotFound();
        await _userRepository.DeleteUser(userInfo);
        return NoContent();
    }

    #endregion [DELETE]

    //[HttpPut]
    //public async Task<IActionResult> UpdatePassword(UserDTO dto, CancellationToken cancellationToken = default)
    //{
    //    if(!string.IsNullOrEmpty(dto.Password))
    //    {
    //        var user = await _userRepository.FindByIdAsync(dto.Id, cancellationToken);
    //        if (user is null) return NotFound();
    //        _mapper.Map(dto, user);
    //        await _userRepository.SaveChangesAsync(cancellationToken);
    //        return NoContent();
    //    } else { return NotFound(); }
    //}

    //[HttpDelete("{id}")]
    //public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken = default)
    //{
    //    var user = await _userRepository.FindByIdAsync(id, cancellationToken);
    //    if (user is null) return NotFound();
    //    using var intranetTransaction = await _intranetContext.Database.BeginTransactionAsync();
    //    foreach (var chatMessage in _chatMessageRepository.FindAll(cm => cm.User.Id == user.Id))
    //    {
    //        _chatMessageRepository.Delete(chatMessage);
    //    }

    //    var conversationIdToDelete = new List<int>();
    //    foreach (var userConversation in _userConversationRepository.FindAll(uc => uc.UserId == user.Id))
    //    {
    //        conversationIdToDelete.Add(userConversation.ConversationId);
    //    }

    //    foreach (var conversationId in conversationIdToDelete)
    //    {
    //        var conversation = await _conversationRepository.FindByIdAsync(conversationId, cancellationToken);
    //        _conversationRepository.Delete(conversation);
    //    }
    //    foreach (var conversationId in conversationIdToDelete)
    //    {
    //        var userConversation = await _userConversationRepository
    //                                        .FindAll(uc => uc.ConversationId == conversationId)
    //                                        .FirstOrDefaultAsync(cancellationToken);
    //        _userConversationRepository.Delete(userConversation);
    //    }
    //    _userRepository.Delete(user);
    //    await _userRepository.SaveChangesAsync(cancellationToken);
    //    await _intranetContext.Database.CommitTransactionAsync(cancellationToken);
    //    return NoContent();
    //}
}