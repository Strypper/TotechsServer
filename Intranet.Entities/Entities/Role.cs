using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Intranet.Entities;

public class Role : IdentityRole
{
    public string RoleIcon { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public string Mission { get; set; } = default!;
    public string MainTasks { get; set; } = default!;

    public ICollection<RoleLevel> RoleLevels { get; set; } = new HashSet<RoleLevel>();
    public virtual ICollection<UserRole> UserRoles { get; } = new HashSet<UserRole>();
}
