using Intranet.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Contract;

public interface IUserRepository
{
    IQueryable<User> FindAll(Expression<Func<User, bool>>? predicate = null!);
    Task<IdentityResult> CreateAccount(User user, string password);
    Task<User?> FindByUserIdWithoutCancellationToken(int id);
    Task<User?> FindByUserNameAsync(string userName, CancellationToken cancellationToken = default);
    Task<User?> FindBySignalRConnectionId(string connectionId, CancellationToken cancellationToken = default);
    Task<User?> FindByGuidAsync(string guid, CancellationToken cancellationToken = default);
    Task<IList<string>> GetRolesAsync(User user);
    Task<IList<Claim>> GetClaimsAsync(User user);

    Task UpdateUser(User user, CancellationToken cancellationToken = default);
    Task DeleteUser(User user, CancellationToken cancelationToken = default);
}
