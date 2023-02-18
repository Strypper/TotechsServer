using System;

namespace Intranet.Entities;

public class ProjectTask : BaseEntity<int>
{
    public string? Title { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; } = DateTime.MinValue;

    public User Author { get; set; } = default!;

    public DateTime? Deadline { get; set; } = DateTime.MaxValue;

    public Priority Priority { get; set; }
}

public enum FeatureTaskStatus { }

public enum TaskPriority { }
