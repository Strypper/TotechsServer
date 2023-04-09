namespace Intranet;

public class AuthenticationController : BaseController
{
    #region [ Fields ]
    private IMapper _mapper;
    private readonly IJWTTokenService _jwtTokenService;
    private readonly IUserRepository _userRepository;
    private readonly IMediaService _mediaService;
    #endregion

    #region [ CTor ]
    public AuthenticationController(IMapper mapper,
                                    IJWTTokenService jwtTokenService,
                                    IUserRepository userRepository,
                                    IMediaService mediaService)
    {
        _mapper = mapper;
        _jwtTokenService = jwtTokenService;
        _userRepository = userRepository;
        _mediaService = mediaService;
    }
    #endregion

    #region [ POST ]
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

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register([FromForm] UserSignUpDTO dto, CancellationToken cancellationToken = default)
    {
        var user = new User()
        {
            UserName = dto.username,
            Email = dto.email
        };
        var result = await _userRepository.CreateAccount(user, dto.password);
        if (result.Succeeded)
        {
            if (dto.avatarfile is not null && _mediaService.IsImage(dto.avatarfile))
            {
                using (Stream stream = dto.avatarfile.OpenReadStream())
                {
                    Tuple<bool, string> uploadresults = await _mediaService.UploadAvatarToStorage(stream, dto.avatarfile.FileName);
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
            else return Ok();
        }
        return BadRequest();
    }
    #endregion [ POST ]

    #region [ PUT ]
    //Forgot password
    #endregion [ PUT ]
}
