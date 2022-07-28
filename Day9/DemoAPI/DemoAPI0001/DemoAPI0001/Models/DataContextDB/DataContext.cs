using DemoAPI0001.Models.Class;
using Microsoft.EntityFrameworkCore;

namespace DemoAPI0001.Models.DataContextDB
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Character>().HasData(

                new Character { Id = 1, Name = "Draco", Job = "Warrior" },
                 new Character { Id = 2, Name = "William", Job = "Archer" },
                  new Character { Id = 3, Name = "John", Job = "Wizard" },
                   new Character { Id = 4, Name = "Mark", Job = "Fighter" }
                );
        }


        public DbSet<Character> Characters { set; get; }

        
    }
}
