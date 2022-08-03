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
        public DbSet<Character> Characters { set; get; }
        public DbSet<Job> Jobs { set; get; }

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

            modelBuilder.Entity<Character>(e =>
            {
                e.HasKey(c => c.Id);

                e.Property(c => c.Id).UseIdentityColumn();

                e.HasIndex(c => c.Name).IsUnique();

                e.Property(c => c.Name).HasColumnType("nvarchar(255)").HasMaxLength(255).IsRequired();

                e.Property(u => u.level).HasDefaultValue(1);

                e.HasOne<User>(c => c.User)
                .WithMany(u => u.Characters)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Job>(e =>
            {
                e.HasKey(j => j.JobId);

                e.Property(j => j.JobId).UseIdentityColumn();

                e.Property(j => j.Name).HasColumnType("nvarchar(50)").HasMaxLength(50).IsRequired();

                e.Property(j => j.Description).HasColumnType("nvarchar(500)").HasMaxLength(500).IsRequired();

                e.HasOne<Character>(j => j.Character)
                .WithOne(c => c.Job)
                .HasForeignKey<Character>(c => c.JobWorkId)
                .HasConstraintName("FK_Job_Character")
                .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
