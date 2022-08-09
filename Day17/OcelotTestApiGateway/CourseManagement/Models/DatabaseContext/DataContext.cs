using CourseManagement.Models.DatabaseModel;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Models.DatabaseContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Course> Courses { set; get; }
    }
}
