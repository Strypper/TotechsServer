using Intranet.AppSettings;
using Intranet.Contract;
using Intranet.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Intranet;

public class JWTTokenService : IJWTTokenService
{
    #region [Fields]
    private readonly IUserRepository _userRepository;
    private readonly JwtTokenConfig _tokenConfig;
    #endregion

    #region [CTor]
    public JWTTokenService(IUserRepository userRepository,
                           IOptionsMonitor<JwtTokenConfig> tokenConfigOptionsAccessor)
    {
        _userRepository = userRepository;
        _tokenConfig = tokenConfigOptionsAccessor.CurrentValue;

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

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfig.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var securityToken = handler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _tokenConfig.Issuer,
            Audience = "mauisland",
            SigningCredentials = creds,
            Subject = identity,
            Expires = DateTime.UtcNow.AddDays(1)
        });

        return handler.WriteToken(securityToken);
    }

    #endregion
}
