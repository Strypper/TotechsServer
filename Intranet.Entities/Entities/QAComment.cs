namespace Intranet.Entities;

public class QAComment :BaseEntity<int>
{
    public bool IsAnswered { get; set; }
    public string Comment { get; set; } = string.Empty;
    public string UserId { get; set; } = default!;
    public User User { get; set; } = default!;
    public int QAId { get; set; }
    public QA QA { get; set; } = default!;
}