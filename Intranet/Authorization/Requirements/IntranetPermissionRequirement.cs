using Microsoft.AspNetCore.Authorization;

namespace Intranet.Authorization.Requirements
{
    public class IntranetPermissionRequirement : IAuthorizationRequirement
    {
        public IntranetPermissionRequirement(string permissions) => Permissions = permissions;
        public string Permissions { get; }
    }
}
