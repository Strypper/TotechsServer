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

public class UserRepository : BaseRepository<User>, IUserRepository
{
    #region [Fields]
    private readonly UserManager _userManager;
    #endregion

    #region [CTor]
    public UserRepository(IntranetContext repositoryContext, UserManager userManager) : base(repositoryContext)
    {
        _userManager = userManager;
    }

    #endregion

    #region [Overrides]
    public override IQueryable<User> FindAll(Expression<Func<User, bool>> predicate = null)
    {
        return _userManager.FindAll(predicate);
    }
    #endregion

    #region [Methods]
    public Task<IdentityResult> CreateAccount(User user, string password)
        => _userManager.CreateAsync(user, password);

    public Task<User> FindByGuidAsync(string guid, CancellationToken cancellationToken = default)
    {
        return _userManager.FindByGuidAsync(guid, cancellationToken);
    }

    public Task<User> FindBySignalRConnectionId(string connectionId, CancellationToken cancellationToken = default)
    {
        return _userManager.FindBySignalRConnectionIdAsync(connectionId, cancellationToken);
    }

    public Task<User> FindByUserIdWithoutCancellationToken(int id)
    {
        throw new System.NotImplementedException();
    }

    public Task<User> FindByUserNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        return _userManager.FindByNameAsync(userName);
    }

    public Task<IList<Claim>> GetClaimsAsync(User user)
        => _userManager.GetClaimsAsync(user);

    public Task<IList<string>> GetRolesAsync(User user)
        => _userManager.GetRolesAsync(user);
    #endregion
}
