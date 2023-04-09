using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Intranet;

public class JWTTokenService : IJWTTokenService
{
    #region [Fields]
    private readonly IUserRepository _userRepository;
    private readonly JwtTokenConfig _tokenConfig;
    #endregion

    #region [CTor]
    public JWTTokenService(IUserRepository userRepository,
                           IOptionsMonitor<JwtTokenConfig> tokenConfig)
    {
        _userRepository = userRepository;
        _tokenConfig = tokenConfig.CurrentValue;
    }
    #endregion

    #region [Methods]
    public async Task<string> GenerateToken(User user)
    {
        var handler = new JwtSecurityTokenHandler();

        var roles = await _userRepository.GetRolesAsync(user);
        var claims = await _userRepository.GetClaimsAsync(user);

        var identity = new ClaimsIdentity(
            new GenericIdentity(user.UserName, JWTConstants.GenericIdentityType),
            new[] {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("guid", user.Id)
                  }
                .Union(roles.Select(role => new Claim(ClaimTypes.Role, role)))
                .Union(claims)
            );

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("54653216554114442313244544"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var securityToken = handler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _tokenConfig.Issuer,
            Audience = _tokenConfig.Audience,
            SigningCredentials = creds,
            Subject = identity,
            Expires = DateTime.UtcNow.AddDays(1)
        });

        return handler.WriteToken(securityToken);
    }

    #endregion
}
