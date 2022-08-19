using Microsoft.EntityFrameworkCore;
using TestWebAPiWithXUnitTest.Models.DatabaseModels;

namespace TestWebAPiWithXUnitTest.Models.DatabaseContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Character> Characters { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Character>(e =>
            {
                e.HasKey(c => c.Id);

                e.Property(c => c.Id).UseIdentityColumn();

                e.HasIndex(c => c.CharacterName).IsUnique();

                e.Property(c => c.CharacterName).HasColumnType("nvarchar(50)").HasColumnName("CharacterName").IsRequired();

                e.Property(c => c.CharacterJob).HasColumnType("nvarchar(30)").HasColumnName("CharacterJob").IsRequired();

                e.Property(c => c.CharacterLevel).HasDefaultValue<int>(1).HasColumnName("CharacterLevel").IsRequired();
            });
        }
    }
}