using Microsoft.EntityFrameworkCore;
using RefreshTokenJwtAuthentication.Models.DatabaseModels;

namespace RefreshTokenJwtAuthentication.Models.DatabaseContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { set; get; }
        public DbSet<RefreshToken> RefreshTokens { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(u => u.Id);

                e.HasIndex(u => u.UserName).IsUnique();

                e.Property(u => u.Id).UseIdentityColumn();

                e.Property(u => u.UserName).HasColumnType("nvarchar(100)").IsRequired();

                e.Property(u => u.Password).HasColumnType("nvarchar(200)").IsRequired();
            });
        }
    }
}
