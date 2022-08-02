using Microsoft.EntityFrameworkCore;
using RefreshToken0011.Models.DatabaseContext;

namespace RefreshToken0011.Models.ModelDatabase
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

                e.Property(u => u.Id).UseIdentityColumn();

                e.HasIndex(u => u.UserName).IsUnique();

                e.Property(u => u.UserName).HasColumnType("nvarchar(255)").HasMaxLength(255).IsRequired();

                e.Property(u => u.Password).HasColumnType("nvarchar(300)").HasMaxLength(300).IsRequired();
            });

            modelBuilder.Entity<RefreshToken>(e =>
            {
                e.HasKey(rt => rt.Id);

                e.Property(rt => rt.Id).UseIdentityColumn().IsRequired();

                e.Property(rt => rt.UserId).IsRequired();

                e.Property(rt => rt.Token).HasColumnType("varchar(1000)").IsRequired();

                e.Property(rt => rt.ExpirationDate).IsRequired();
            });
        }
    }
}
