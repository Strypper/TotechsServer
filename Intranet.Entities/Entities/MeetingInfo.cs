namespace Intranet.Entities;

public class MeetingInfo : BaseEntity<int>
{
    public string Description { get; set; }
    public string Location { get; set; }
    public ImportanceLevel ImportanceLevel { get; set; }
}

public enum ImportanceLevel
{
    Chill, Medium, High, Extreme
}