using Intranet.Contract;
using Intranet.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Repo;

public class UserRepository : IUserRepository
{
    #region [Fields]
    private readonly UserManager _userManager;
    #endregion

    #region [CTor]
    public UserRepository(UserManager userManager)
    {
        _userManager = userManager;
    }

    #endregion

    #region [Methods]
    public Task<IdentityResult> CreateAccount(User user, string password)
        => _userManager.CreateAsync(user, password);

    public Task<User?> FindByGuidAsync(string guid, CancellationToken cancellationToken = default)
        => _userManager.FindByGuidAsync(guid, cancellationToken);

    public Task<User?> FindBySignalRConnectionId(string connectionId, CancellationToken cancellationToken = default)
        => _userManager.FindBySignalRConnectionIdAsync(connectionId, cancellationToken);

    public Task<User?> FindByUserIdWithoutCancellationToken(int id)
    {
        throw new System.NotImplementedException();
    }

    public Task<User?> FindByUserNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        return _userManager.FindByNameAsync(userName);
    }

    public Task<IList<Claim>> GetClaimsAsync(User user)
        => _userManager.GetClaimsAsync(user);

    public Task<IList<string>> GetRolesAsync(User user)
        => _userManager.GetRolesAsync(user);

    public async Task DeleteUser(User user, CancellationToken cancelationToken = default)
        => await _userManager.DeleteAsync(user);

    public async Task UpdateUser(User user, CancellationToken cancellationToken = default)
        => await _userManager.UpdateAsync(user);

    public IQueryable<User> FindAll(Expression<Func<User, bool>>? predicate = null)
        => _userManager.FindAll(predicate!);

    #region [Pending]

    #endregion
    #endregion
}
