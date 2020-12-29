using Intranet.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Intranet.Entities.Database
{
    public class IntranetContext : DbContext
    {
        public IntranetContext(DbContextOptions<IntranetContext> options) : base(options) { }
        public DbSet<Food> Foods { get; set; }
        public DbSet<User> Users { get; set; }
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
                entity.Property(e => e.Password).IsRequired(true);
            });
        }
    }
}
