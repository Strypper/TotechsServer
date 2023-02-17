namespace Intranet;

[Route("/api/[controller]/[action]")]
public class AuthenticationController : BaseController
{
    #region [Fields]
    private IMapper _mapper;
    private readonly IJWTTokenService _jwtTokenService;
    private readonly IUserRepository _userRepository;
    #endregion

    #region [CTor]
    public AuthenticationController(IMapper mapper,
                                    IJWTTokenService jwtTokenService,
                                    IUserRepository userRepository)
    {
        _mapper = mapper;
        _jwtTokenService = jwtTokenService;
        _userRepository = userRepository;
    }
    #endregion

    #region [GET]
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
    #endregion

    #region [POST]
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(UserLoginDTO userLoginDTO, CancellationToken cancellationToken = default)
    {

        var user = await _userRepository.FindByUserNameAsync(userLoginDTO.username, cancellationToken);
        if (user is null || user.IsDisable == true) return NotFound();


        var accessToken = await _jwtTokenService.GenerateToken(user);
        var requestAt = DateTime.UtcNow;
        var expiredIn = Math.Floor((requestAt.AddDays(1) - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);
        return Ok(new
        {
            user.Id,
            requestAt,
            accessToken,
            expiredIn
        });
    }



    [HttpPost]
    public async Task<IActionResult> Register(UserSignUpDTO dto, CancellationToken cancellationToken = default)
    {
        var user = new User()
        {
            UserName = dto.username,
            Email = dto.email,
            ProfilePic = dto.avatarurl,
        };
        var result = await _userRepository.CreateAccount(user, dto.password);
        if (result.Succeeded)
            return NoContent();
        return BadRequest(result);
    }
    #endregion

    #region [PUT]

    #endregion

    #region [DELETE]

    #endregion
}
