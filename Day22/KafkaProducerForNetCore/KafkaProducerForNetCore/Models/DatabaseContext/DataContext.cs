using KafkaProducerForNetCore.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace KafkaProducerForNetCore.Models.DatabaseContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>(e =>
            {
                e.HasKey(e => e.Id);

                e.Property(e => e.EmployeeName).HasColumnType("nvarchar(50)").IsRequired();

                e.Property(e => e.EmployeeClass).HasColumnType("nvarchar(30)").IsRequired();
            });
        }
    }
}
