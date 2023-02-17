namespace Intranet.Entities;

public class MediaAssets : BaseEntity<int>
{
    public string? Title { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;

    public MediaAssestType MediaAssestType { get; set; }
}

public enum MediaAssestType { }
