using Intranet.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Intranet.Entities.Database
{
    public class IntranetContext : DbContext
    {
        public IntranetContext(DbContextOptions<IntranetContext> options) : base(options) { }
        public DbSet<Food>             Foods             { get; set; }
        public DbSet<User>             Users             { get; set; }
        public DbSet<Project>          Projects          { get; set; }
        public DbSet<Conversation>     Conversations     { get; set; }
        public DbSet<ChatMessage>      ChatMessages      { get; set; }
        public DbSet<UserFood>         UserFoods         { get; set; }
        public DbSet<UserProject>      UserProjects      { get; set; }
        public DbSet<UserConversation> UserConversations { get; set; }
        public DbSet<Contribution>     Contributions     { get; set; }
        public DbSet<MeetingSchedule>  MeetingSchedules  { get; set; }
        public DbSet<MeetingInfo>      MeetingInfos      { get; set; }
        public DbSet<Attendance>       Attendances       { get; set; }
        public DbSet<TodoTask>         TodoTasks         { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Food>(entity =>
            {
                entity.Property(e => e.MainIcon).IsRequired(true);
            });

            builder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserName).IsRequired(true);
                entity.Property(e => e.FirstName).IsRequired(false);
                entity.Property(e => e.MiddleName).IsRequired(false);
                entity.Property(e => e.LastName).IsRequired(false);
            });

            builder.Entity<UserFood>(entity =>
            {
                entity.ToTable("UserFoods");
                entity.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserId);
                entity.HasOne(e => e.Food).WithMany().HasForeignKey(e => e.FoodId);
            });

            builder.Entity<UserProject>(entity =>
            {
                entity.ToTable("UserProjects");
                entity.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserId);
                entity.HasOne(e => e.Project).WithMany().HasForeignKey(e => e.ProjectId);
            });

            builder.Entity<UserConversation>(entity =>
            {
                entity.ToTable("UserConversations");
                entity.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserId);
                entity.HasOne(e => e.Conversation).WithMany().HasForeignKey(e => e.ConversationId);
            });
        }
    }
}
