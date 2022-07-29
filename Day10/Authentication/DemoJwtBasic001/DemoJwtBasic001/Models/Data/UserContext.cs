using DemoJwtBasic001.Models.Model;
using Microsoft.EntityFrameworkCore;

namespace DemoJwtBasic001.Models.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(
                
                entity =>
                {
                    entity.HasKey(u => u.Id);
                    entity.HasIndex(u => u.Email).IsUnique();
                    entity.Property(u => u.Name).HasColumnType("nvarchar(50)").HasMaxLength(30);
                    entity.Property(u => u.Email).HasColumnType("nvarchar(200)").HasMaxLength(100);
                    entity.Property(u => u.Password).HasColumnType("nvarchar(500)").HasMaxLength(500);
                }
                
                );
        }

        public DbSet<User> Users { get; set; }
    }
}
