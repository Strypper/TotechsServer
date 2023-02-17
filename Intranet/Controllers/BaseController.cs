namespace Intranet;

[Authorize(AuthenticationSchemes = "Bearer")]
[ApiController]
public class BaseController : ControllerBase { }
