using Intranet.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Intranet.Entities.Database
{
    public class IntranetContext : DbContext
    {
        public IntranetContext(DbContextOptions<IntranetContext> options) : base(options) { }
        public DbSet<Food> Foods { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<UserFood> UserFoods { get; set; }

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
                entity.Property(e => e.Password).IsRequired(true);
            });

            builder.Entity<UserFood>(entity =>
            {
                entity.ToTable("UserFoods");
                entity.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserId);
                entity.HasOne(e => e.Food).WithMany().HasForeignKey(e => e.FoodId);
            });

            builder.Entity<UserTeam>(entity =>
            {
                entity.ToTable("UserTeams");
                entity.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserId);
                entity.HasOne(e => e.Team).WithMany().HasForeignKey(e => e.TeamId);
            });
        }
    }
}
