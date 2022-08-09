using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models.ModelClass;

namespace StudentManagementSystem.Models.DatabaseContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>(e =>
            {
                e.HasKey(s => s.Id);

                e.Property(s => s.Id).UseIdentityColumn();

                e.Property(s => s.StudentName).HasColumnType("nvarchar(50)").IsRequired();

                e.Property(s => s.StudentAge).HasDefaultValue(1).IsRequired();

                e.Property(s => s.StudentAddress).HasColumnType("nvarchar(150)").IsRequired();

                e.Property(s => s.StudentPhone).IsRequired();
            });
        }
    }
}
