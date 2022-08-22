using Microsoft.EntityFrameworkCore;
using TestWebAPiWithXUnitTest.Models.DatabaseModels;

namespace TestWebAPiWithXUnitTest.Models.DatabaseContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Character> Characters { set; get; }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1">DbSet</see> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">
        /// The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.
        /// </param>
        /// <remarks>
        ///   <para>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)">UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)</see>)
        /// then this method will not be run.
        /// </para>
        ///   <para>
        /// See <a href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</a> for more information.
        /// </para>
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Character>(e =>
            {
                e.HasKey(c => c.Id);

                e.Property(c => c.Id).UseIdentityColumn();

                e.HasIndex(c => c.CharacterName).IsUnique();

                e.Property(c => c.CharacterName).HasColumnType("nvarchar(50)").HasColumnName("CharacterName").IsRequired();

                e.Property(c => c.CharacterJob).HasColumnType("nvarchar(30)").HasColumnName("CharacterJob").IsRequired();

                e.Property(c => c.CharacterLevel).HasDefaultValue<int>(1).HasColumnName("CharacterLevel").IsRequired();
            });
        }
    }
}