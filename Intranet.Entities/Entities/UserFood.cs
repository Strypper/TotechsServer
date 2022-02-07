namespace Intranet.Entities.Entities
{
    public class UserFood : BaseEntity
    {
        public int UserId { get; set; }
        public int FoodId { get; set; }
        public virtual User User { get; set; }
        public virtual Food Food { get; set; }
    }
}
