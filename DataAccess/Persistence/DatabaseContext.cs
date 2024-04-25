
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Core.Entiteti;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccess.Persistence
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration configuration;
        //public DatabaseContext(DbContextOptions<DatabaseContext> options, IConfiguration configuration)
        //    : base(options)
        //{
        //    this.configuration = configuration;
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-DMLS484\\MSSQLSERVER01;Initial Catalog=ShakFoodDb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Restaurant_Food>()
                .HasKey(rf => new { rf.Food_ID, rf.Restaurant_ID });

            modelBuilder.Entity<Restaurant_Food>()
                .HasOne(rf => rf.Food)
                .WithMany()
                .HasForeignKey(rf => rf.Food_ID);

            modelBuilder.Entity<Restaurant_Food>()
                .HasOne(rf => rf.Restaurant)
                .WithMany()
                .HasForeignKey(rf => rf.Restaurant_ID);
        }
        public DbSet<City> City { get; set; }
        public DbSet<Food> Food { get; set; }
        public DbSet<Food_category> Food_category { get; set; }
        public DbSet<Restaurant> Restaurant { get; set; }
        public DbSet<Restaurant_Food> Restaurant_Food { get; set; }
       
    }
   
}