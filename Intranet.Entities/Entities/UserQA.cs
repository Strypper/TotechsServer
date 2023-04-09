namespace Intranet.Entities;

public class UserQA : BaseEntity<int>
{
    public string UserId { get; set; } = default!;
    public int QAId { get; set; }
    public bool IsAuthor { get; set; }
    public virtual User User { get; set; } = default!;
    public virtual QA QA { get; set; } = default!;
}
