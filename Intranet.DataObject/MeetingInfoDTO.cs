namespace Intranet.DataObject;

public class MeetingInfoDTO : BaseDTO<int>
{
    public string? Description { get; set; }
    public string? Location { get; set; }
    public ImportanceLevelDTO ImportanceLevel { get; set; }
}

public enum ImportanceLevelDTO
{
    Chill, Medium, High, Extreme
}