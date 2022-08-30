using Microsoft.EntityFrameworkCore;

namespace TestDockerForWebApi
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { set; get; }
    }
}
