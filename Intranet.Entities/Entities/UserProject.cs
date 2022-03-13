namespace Intranet.Entities.Entities
{
    public class UserProject : BaseEntity
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public virtual User User { get; set; }
        public virtual Project Project { get; set; }
    }
}
