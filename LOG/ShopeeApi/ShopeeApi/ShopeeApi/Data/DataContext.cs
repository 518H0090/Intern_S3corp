using Microsoft.EntityFrameworkCore;
using ShopeeApi.Models;

namespace ShopeeApi.Datas
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Restaurant> Restaurants { set; get; }
        public DbSet<Category> Categories { set; get; }
        public DbSet<Food> Foods { set; get; }
        public DbSet<RelationCategoryFood> RelationCategoryFoods { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //User
            modelBuilder.Entity<User>(user =>
            {
                user.HasKey(x => x.Id);

                user.HasIndex(x => x.UserName).IsUnique();

                user.Property(u => u.Id).UseIdentityColumn();

                user.Property(u => u.UserName).HasColumnType("nvarchar(50)").IsRequired();

                user.Property(u => u.PasswordHash).IsRequired();

                user.Property(u => u.PasswordSalt).IsRequired();
            });

            //Restaurant
            modelBuilder.Entity<Restaurant>(res =>
            {
                res.HasKey(x => x.RsId);

                res.HasIndex(x => x.RsTitle).IsUnique();

                res.Property(x => x.RsId).UseIdentityColumn();

                res.Property(x => x.RsTitle).HasColumnType("nvarchar(100)").IsRequired();

                res.Property(x => x.RsProvince).HasColumnType("nvarchar(200)").IsRequired();

                res.Property(x => x.RsAddress).HasColumnType("nvarchar(200)").IsRequired();

                res.Property(x => x.RsType).HasColumnType("nvarchar(30)").IsRequired();

                res.Property(x => x.RsImageUrl).HasColumnType("nvarchar(600)").IsRequired();

                res.Property(x => x.RsStar).HasDefaultValue<float>(1);

                res.Property(x => x.RsReviews).HasDefaultValue<int>(0);

                res.Property(x => x.RsOpenTime).HasColumnType("nvarchar(100)").IsRequired();

                res.Property(x => x.RsPrinceRange).HasColumnType("nvarchar(800)").IsRequired();

                res.Property(x => x.RsRefeLike).HasDefaultValue<bool>(false);

                res.Property(x => x.RsPromotion).IsRequired();

                res.HasMany<Category>(res => res.Categories)
                .WithOne(cate => cate.Restaurant)
                .HasForeignKey(cate => cate.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);

                res.HasMany<Food>(res => res.Foods)
                .WithOne(f => f.Restaurant)
                .HasForeignKey(f => f.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade)
                ;
            });

            //Restaurant Category
            modelBuilder.Entity<Category>(cate =>
            {
                cate.HasKey(x => x.CategoryId);

                cate.HasIndex(x => new { x.CategoryTag, x.RestaurantId }).IsUnique();

                cate.Property(x => x.CategoryId).UseIdentityColumn();

                cate.Property(x => x.CategoryName).HasColumnType("nvarchar(200)").IsRequired();

                cate.Property(x => x.CategoryTag).HasColumnType("nvarchar(50)").IsRequired();

                cate.HasOne<Restaurant>(cate => cate.Restaurant)
                .WithMany(res => res.Categories)
                .HasForeignKey(cate => cate.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);

                cate.HasMany<Food>(cate => cate.Foods)
                .WithMany(cate => cate.Categories)
                .UsingEntity<RelationCategoryFood>(
                    x =>
                    {
                        x.HasOne<Category>(rcf => rcf.Category)
               .WithMany(c => c.RelationCategoryFoods)
               .HasForeignKey(rcf => rcf.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);

                        x.HasOne<Food>(rcf => rcf.Food)
                       .WithMany(c => c.RelationCategoryFoods)
                       .HasForeignKey(rcf => rcf.FoodId)
                       .OnDelete(DeleteBehavior.Restrict);
                    }

                    );
            });

            //Food 
            modelBuilder.Entity<Food>(food =>
            {
                food.HasKey(x => x.FoodId);

                food.HasIndex(x => new { x.FoodTitle, x.RestaurantId }).IsUnique();

                food.Property(x => x.FoodId).UseIdentityColumn();

                food.Property(x => x.FoodImageUrl).HasColumnType("nvarchar(500)").IsRequired();

                food.Property(x => x.FoodTitle).HasColumnType("nvarchar(100)").IsRequired();

                food.Property(x => x.FoodTitle).HasColumnType("nvarchar(500)").IsRequired();

                food.Property(x => x.FoodPrice).HasDefaultValue<int>(0).IsRequired();

                food.Property(x => x.FoodPriceLess).HasDefaultValue<float>(0).IsRequired();

                food.HasOne<Restaurant>(f => f.Restaurant)
                .WithMany(res => res.Foods)
                .HasForeignKey(f => f.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);

                food.HasMany<Category>(f => f.Categories)
                .WithMany(cate => cate.Foods)
                .UsingEntity<RelationCategoryFood>(
                    x =>
                    {
                        x.HasOne<Category>(rcf => rcf.Category)
                        .WithMany(c => c.RelationCategoryFoods)
                        .HasForeignKey(rcf => rcf.CategoryId)
                        .OnDelete(DeleteBehavior.Restrict);

                        x.HasOne<Food>(rcf => rcf.Food)
                       .WithMany(c => c.RelationCategoryFoods)
                       .HasForeignKey(rcf => rcf.FoodId)
                       .OnDelete(DeleteBehavior.Restrict);
                    }
                    
                    );
            });

            //RelationCategoryFood
            modelBuilder.Entity<RelationCategoryFood>(x =>
            {
                x.HasKey(rcf => new { rcf.CategoryId, rcf.FoodId });

                x.HasOne<Category>(rcf => rcf.Category)
                .WithMany(c => c.RelationCategoryFoods)
                .HasForeignKey(rcf => rcf.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

                x.HasOne<Food>(rcf => rcf.Food)
               .WithMany(c => c.RelationCategoryFoods)
               .HasForeignKey(rcf => rcf.FoodId)
               .OnDelete(DeleteBehavior.Restrict);
            });



        }
    }
}