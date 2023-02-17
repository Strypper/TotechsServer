using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Intranet.Entities;
public class IntranetContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
{
    public IntranetContext(DbContextOptions options) : base(options) { }
    public DbSet<Food> Foods { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Conversation> Conversations { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<UserFood> UserFoods { get; set; }
    public DbSet<RoleLevel> RoleLevels { get; set; }
    public DbSet<UserProject> UserProjects { get; set; }
    public DbSet<UserConversation> UserConversations { get; set; }
    public DbSet<Contribution> Contributions { get; set; }
    public DbSet<MeetingSchedule> MeetingSchedules { get; set; }
    public DbSet<MeetingInfo> MeetingInfos { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<TodoTask> TodoTasks { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Food>(entity =>
        {
            entity.Property(e => e.MainIcon).IsRequired(true);
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

