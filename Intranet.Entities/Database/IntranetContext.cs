using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Intranet.Entities;
public class IntranetContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
{
    public IntranetContext(DbContextOptions options) : base(options) { }
    public DbSet<Food> Foods { get; set; } = default!;
    public DbSet<Project> Projects { get; set; } = default!;
    public DbSet<Conversation> Conversations { get; set; } = default!;
    public DbSet<ChatMessage> ChatMessages { get; set; } = default!;
    public DbSet<UserFood> UserFoods { get; set; } = default!;
    public DbSet<RoleLevel> RoleLevels { get; set; } = default!;
    public DbSet<UserProject> UserProjects { get; set; } = default!;
    public DbSet<UserConversation> UserConversations { get; set; } = default!;
    public DbSet<Contribution> Contributions { get; set; } = default!;
    public DbSet<MeetingSchedule> MeetingSchedules { get; set; } = default!;
    public DbSet<MeetingInfo> MeetingInfos { get; set; } = default!;
    public DbSet<Attendance> Attendances { get; set; } = default!;
    public DbSet<TodoTask> TodoTasks { get; set; } = default!;

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

        builder.Entity<ChatMessage>()
               .HasOne(c => c.User)
               .WithMany(u => u.ChatMessages)
               .OnDelete(DeleteBehavior.Cascade);
    }
}

