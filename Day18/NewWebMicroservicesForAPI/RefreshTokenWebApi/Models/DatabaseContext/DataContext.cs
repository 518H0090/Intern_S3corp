using Microsoft.EntityFrameworkCore;
using RefreshTokenWebApi.Models.DatabaseModels;

namespace RefreshTokenWebApi.Models.DatabaseContext
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

                e.Property(u => u.UserName).HasColumnType("nvarchar(50)").IsRequired();

                e.Property(u => u.Password).IsRequired();
            });

            modelBuilder.Entity<RefreshToken>(e =>
            {
                e.HasKey(rt => rt.Id);

                e.Property(rt => rt.UserName).HasColumnType("nvarchar(50)").IsRequired();

                e.Property(u => u.AccessToken).IsRequired();

                e.Property(u => u.ExpiresToken).IsRequired();
            });
        }
    }
}
