using Intranet.Entities;
using System;

namespace Intranet.DataObject;

public class FeatureDTO : BaseDTO<int>
{
    public string? Icon { get; set; } = string.Empty;

    public FeatureStatus FeatureStatus { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.MinValue;

    public string? Description { get; set; } = string.Empty;

    public string? Url { get; set; } = string.Empty;

    public DateTime? Deadline { get; set; } = DateTime.MaxValue;

    public string? Title { get; set; } = string.Empty;

    public Priority Priority { get; set; }
}
