using System;
using System.Collections.Generic;

namespace Intranet.Entities;

public class Feature : BaseEntity<int>
{
    public string? Icon { get; set; } = string.Empty;

    public FeatureStatus FeatureStatus { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.MinValue;

    public string? Description { get; set; } = string.Empty;

    public string? Url { get; set; } = string.Empty;

    public DateTime? Deadline { get; set; } = DateTime.MaxValue;

    public string? Title { get; set; } = string.Empty;

    public Priority Priority { get; set; }

    public ICollection<MediaAssets>? MediaAssests { get; set; } = new HashSet<MediaAssets>();

    public ICollection<SubFeature> SubFeatures { get; set; } = new HashSet<SubFeature>();


}

public enum FeatureStatus { }
public enum Priority { }
