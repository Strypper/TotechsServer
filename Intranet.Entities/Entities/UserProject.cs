namespace Intranet.Entities;

public class UserProject : BaseEntity<int>
{
    public string UserId { get; set; } = default!;
    public int ProjectId { get; set; }
    public virtual User User { get; set; } = default!;
    public virtual Project Project { get; set; } = default!;
}
