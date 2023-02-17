using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Intranet;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class BaseController : ControllerBase { }
