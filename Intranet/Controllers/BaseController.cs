using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Intranet;

[ApiController]
[Route("/api/[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class BaseController : ControllerBase { }
