namespace Intranet.Entities;

public class MeetingInfo : BaseEntity<int>
{
    public string Description { get; set; } = default!;
    public string Location { get; set; } = default!;
    public ImportanceLevel ImportanceLevel { get; set; }
}

public enum ImportanceLevel
{
    Chill, Medium, High, Extreme
}