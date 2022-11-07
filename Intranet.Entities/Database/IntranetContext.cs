using Intranet.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Intranet.Entities.Database
{
    public class IntranetContext : DbContext
    {
        public IntranetContext(DbContextOptions<IntranetContext> options) : base(options) { }
        public DbSet<Food>              Foods               { get; set; }
        public DbSet<User>              Users               { get; set; }
        public DbSet<Interest>          Interests           { get; set; }
        public DbSet<Skill>             Skills              { get; set; }
        public DbSet<Expertise>         Expertises          { get; set; }
        public DbSet<Certification>     Certifications      { get; set; }
        public DbSet<Project>           Projects            { get; set; }
        public DbSet<Conversation>      Conversations       { get; set; }
        public DbSet<ChatMessage>       ChatMessages        { get; set; }
        public DbSet<UserFood>          UserFoods           { get; set; }
        public DbSet<UserProject>       UserProjects        { get; set; }
        public DbSet<UserConversation>  UserConversations   { get; set; }
        public DbSet<Contribution>      Contributions       { get; set; }
        public DbSet<MeetingSchedule>   MeetingSchedules    { get; set; }
        public DbSet<MeetingInfo>       MeetingInfos        { get; set; }
        public DbSet<Attendance>        Attendances         { get; set; }
        public DbSet<TodoTask>          TodoTasks           { get; set; }  


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Food>(entity =>
            {
                entity.Property(e => e.MainIcon).IsRequired(true);
            });

            builder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserName).IsRequired(true);
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

            builder.Entity<UserExpertise>(entity =>
            {
                entity.ToTable("UserExpertises");
                entity.HasOne(e => e.User).WithMany(usr => usr.UserExpertises).HasForeignKey(e => e.UserId);
                entity.HasOne(e => e.Expertise).WithMany(exp => exp.UserExpertises).HasForeignKey(e => e.ExpertiseId);

                entity.HasKey(e => new { e.UserId, e.ExpertiseId });
            });

            builder.Entity<UserCertification>(entity =>
            {
                entity.ToTable("UserCertifications");
                entity.HasOne(e => e.User).WithMany(usr => usr.UserCertifications).HasForeignKey(e => e.UserId);
                entity.HasOne(e => e.Certification).WithMany(cer => cer.UserCertifications).HasForeignKey(e => e.CertificationId);

                entity.HasKey(e => new { e.UserId, e.CertificationId });
            }); 

            builder.Entity<UserSkill>(entity =>
            {
                entity.ToTable("UserSkills");
                entity.HasOne(e => e.User).WithMany(usr => usr.UserSkills).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Skill).WithMany(skl => skl.UserSkills).HasForeignKey(e => e.SkillId).OnDelete(DeleteBehavior.Cascade);

                entity.HasKey(e => new { e.SkillId, e.UserId });
            });

            builder.Entity<SkillExpertise>(entity =>
            {
                entity.ToTable("SkillExpertises");
                entity.HasOne(e => e.Expertise).WithMany(exp => exp.SkillExpertises).HasForeignKey(e => e.ExpertiseId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Skill).WithMany(skl => skl.SkillExpertises).HasForeignKey(e => e.SkillId).OnDelete(DeleteBehavior.Cascade);

                entity.HasKey(e => new { e.SkillId, e.ExpertiseId });
            });
        }
    }
}
