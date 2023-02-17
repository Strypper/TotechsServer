using System.Collections.Generic;

namespace Intranet.Entities;

public class SubFeature : BaseEntity<int>
{
    public string? Icon { get; set; } = string.Empty;

    public virtual ICollection<ProjectTask> Tasks { get; set; } = new HashSet<ProjectTask>();
}
