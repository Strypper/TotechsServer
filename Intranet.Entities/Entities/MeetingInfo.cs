namespace Intranet.Entities.Entities
{
    public class MeetingInfo : BaseEntity
    {
        public string          Description     { get; set; }
        public string          Location        { get; set; }
        public ImportanceLevel ImportanceLevel { get; set; }
    }

    public enum ImportanceLevel
    {
        Chill, Medium, High, Extreme
    }
}