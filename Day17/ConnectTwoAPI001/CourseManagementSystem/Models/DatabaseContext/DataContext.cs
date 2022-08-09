using CourseManagementSystem.Models.ModelClass;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Models.DatabaseContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Course> Courses { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>(e =>
            {
                e.HasKey(c => c.Id);

                e.Property(c => c.Id).UseIdentityColumn();

                e.Property(c => c.CourseName).HasColumnType("nvarchar(50)").IsRequired();

                e.Property(c => c.CourseDescription).HasColumnType("nvarchar(50)").IsRequired();
            });
        }
    }
}
