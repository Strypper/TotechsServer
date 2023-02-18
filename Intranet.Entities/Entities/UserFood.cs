namespace Intranet.Entities;

public class UserFood : BaseEntity<int>
{
    public string UserId { get; set; } = default!;
    public int FoodId { get; set; }
    public virtual User User { get; set; } = default!;
    public virtual Food Food { get; set; } = default!;
}
