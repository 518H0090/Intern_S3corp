using DatabaseManagement.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace DatabaseManagement.Models.DatabaseContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { set; get; }
        public DbSet<Course> Courses { set; get; }
        public DbSet<StudentCourse> StudentCourses { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>(e =>
            {
                e.HasKey(s => s.Id);

                e.Property(s => s.StudentName).HasColumnType("nvarchar(50)").IsRequired();

                e.Property(s => s.StudentAddress).HasColumnType("nvarchar(100)").IsRequired();

                e.Property(s => s.StudentPhone).IsRequired();
            });


            modelBuilder.Entity<Course>(e =>
            {
                e.HasKey(c => c.Id);

                e.Property(c => c.CourseName).HasColumnType("nvarchar(50)").IsRequired();

                e.Property(c => c.CourseDescription).IsRequired();
            });

            modelBuilder.Entity<StudentCourse>(e =>
            {
                e.HasKey(sc => new {sc.StudentId, sc.CourseId});

                e.HasOne<Student>(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

                e.HasOne<Course>(sc => sc.Course)
               .WithMany(s => s.StudentCourses)
               .HasForeignKey(sc => sc.CourseId)
               .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
