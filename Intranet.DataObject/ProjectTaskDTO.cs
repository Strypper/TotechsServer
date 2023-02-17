using Intranet.Entities;
using System;

namespace Intranet.DataObject;

public class ProjectTaskDTO : BaseDTO<int>
{
    public string? Title { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; } = DateTime.MinValue;

    public DateTime? Deadline { get; set; } = DateTime.MaxValue;

    public Priority Priority { get; set; }
}
