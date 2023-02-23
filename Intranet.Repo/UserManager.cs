using Intranet.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Repo;

public class UserManager : UserManager<User>
{
    public UserManager(
        IUserStore<User> store,
        IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<User> passwordHasher,
        IEnumerable<IUserValidator<User>> userValidators,
        IEnumerable<IPasswordValidator<User>> passwordValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<UserManager<User>> logger
    ) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
    {
    }

    public async Task<User?> FindByGuidAsync(string guid, CancellationToken cancellationToken)
        => await Users.FirstOrDefaultAsync(u => u.Id == guid, cancellationToken);

    public new async Task<User?> FindByNameAsync(string userName)
    {
        var user = await base.FindByNameAsync(userName);
        if (user is null || user.IsDeleted)
            return null;
        return user;
    }

    public async Task<User?> FindBySignalRConnectionIdAsync(string signalRConnectionId, CancellationToken cancellationToken)
        => await Users.FirstOrDefaultAsync(u => u.SignalRConnectionId == signalRConnectionId, cancellationToken);

    public async Task<User?> FindByPhoneNumberAsync(string phoneNumber)
        => await Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);

    public IQueryable<User> FindAll(Expression<Func<User, bool>>? predicate = null)
        => Users
            .Where(u => !u.IsDeleted)
            .WhereIf(predicate != null, predicate!);
}
