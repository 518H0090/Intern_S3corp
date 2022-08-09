using Microsoft.EntityFrameworkCore;
using StudentManagement.Models.DatabaseModel;

namespace StudentManagement.Models.DatabaseContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { set; get; }
    }
}
