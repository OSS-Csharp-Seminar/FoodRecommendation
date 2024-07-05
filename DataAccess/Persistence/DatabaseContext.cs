using Microsoft.EntityFrameworkCore;
using Core.Entiteti;
using System.Reflection;

namespace DataAccess.Persistence
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Zip).IsRequired().HasMaxLength(10);
                entity.Property(c => c.County).HasMaxLength(100);
                entity.Property(c => c.City_code).HasMaxLength(10);
                entity.HasMany(c => c.Restaurants)
                      .WithOne()
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.ToTable("Restaurant");
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Name).IsRequired().HasMaxLength(100);
                entity.HasOne<City>()
                    .WithMany(x=>x.Restaurants)
                    .HasForeignKey(r => r.CityId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Food_category>(entity =>
            {
                entity.ToTable("Food_category");
                entity.HasKey(fc => fc.Id);
                entity.Property(fc => fc.Category).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Food>(entity =>
            {
                entity.ToTable("Food");
                entity.HasKey(f => f.Id);
                entity.Property(f => f.Name).IsRequired().HasMaxLength(100);
                entity.Property(f => f.Price).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(f => f.Description).HasMaxLength(100);
                entity.HasOne(f => f.Category)
                      .WithMany()
                      .HasForeignKey(f => f.Category_ID);
            });

            modelBuilder.Entity<Restaurant_Food>(entity =>
            {
                entity.ToTable("Restaurant_Food");
                entity.HasKey(rf => new { rf.Food_ID, rf.Restaurant_ID });
                entity.HasOne(rf => rf.Food)
                      .WithMany()
                      .HasForeignKey(rf => rf.Food_ID);
                entity.HasOne(rf => rf.Restaurant)
                      .WithMany()
                      .HasForeignKey(rf => rf.Restaurant_ID);
            });
        }

        public DbSet<City> City { get; set; }
        public DbSet<Food> Food { get; set; }
        public DbSet<Food_category> Food_category { get; set; }
        public DbSet<Restaurant> Restaurant { get; set; }
        public DbSet<Restaurant_Food> Restaurant_Food { get; set; }

        public override int SaveChanges()
        {
            //CascadeUpdate();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //CascadeUpdate();
            return base.SaveChangesAsync(cancellationToken);
        }

        //private void CascadeUpdate()
        //{
        //    var modifiedCities = ChangeTracker.Entries<City>()
        //        .Where(e => e.State == EntityState.Modified)
        //        .Select(e => e.Entity);

        //    foreach (var city in modifiedCities)
        //    {
        //        var restaurants = Restaurant.Where(r => r.CityId == city.Id).ToList();
        //        foreach (var restaurant in restaurants)
        //        {
        //            restaurant.CityId = city.Id;
        //        }
        //    }
        //}
    }
}
