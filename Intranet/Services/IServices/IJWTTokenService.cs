using Intranet.Entities;
using System.Threading.Tasks;

namespace Intranet;

public interface IJWTTokenService
{
    Task<string> GenerateToken(User user);
}
