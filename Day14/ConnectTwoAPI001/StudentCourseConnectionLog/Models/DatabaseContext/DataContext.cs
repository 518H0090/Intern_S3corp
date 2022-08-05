using Microsoft.EntityFrameworkCore;
using StudentCourseConnectionLog.Models.DatabaseModels;

namespace StudentCourseConnectionLog.Models.DatabaseContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<StudentCourse> StudentCourses { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentCourse>(e =>
            {
                e.HasKey(SC => new { SC.StudentId, SC.CourseId } );
            });
        }
    }
}
