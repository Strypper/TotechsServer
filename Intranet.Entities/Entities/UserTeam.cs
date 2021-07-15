namespace Intranet.Entities.Entities
{
    public class UserTeam : BaseEntity
    {
        public int UserId { get; set; }
        public int TeamId { get; set; }
        public virtual User User { get; set; }
        public virtual Team Team { get; set; }
    }
}
