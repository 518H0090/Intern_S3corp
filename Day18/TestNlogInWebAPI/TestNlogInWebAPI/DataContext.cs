using Microsoft.EntityFrameworkCore;

namespace TestNlogInWebAPI
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Character> Characters { set; get; }
    }
}
