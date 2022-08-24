using Microsoft.EntityFrameworkCore;

namespace TestDockerForWebApi
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Student> Students { set; get; }
    }
}
