using CourseManagementSystem.Models.ModelClass;
using Microsoft.EntityFrameworkCore;
using StudentCourseConnectionLog.Models.DatabaseModels;
using StudentManagementSystem.Models.ModelClass;

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

            //modelBuilder.Entity<Student>(e =>
            //{
            //    e.HasMany<StudentCourse>(s => s.StudentCourses)
            //    .WithOne(sc => sc.Student)
            //    .HasForeignKey(sc => sc.StudentId)
            //    .OnDelete(DeleteBehavior.Cascade);
            //});

            //modelBuilder.Entity<Course>(e => {
            //    e.HasMany<StudentCourse>(c => c.StudentCourses)
            //    .WithOne(sc => sc.Course)
            //    .HasForeignKey(sc => sc.CourseId)
            //    .OnDelete(DeleteBehavior.Cascade);
            //});
        }
    }
}
