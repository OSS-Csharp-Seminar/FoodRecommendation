
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Core.Entiteti;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccess.Persistence
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration configuration;
        public DatabaseContext(DbContextOptions<DatabaseContext> options, IConfiguration configuration)
            : base(options)
        {
            this.configuration = configuration;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine($"Connection String: {connectionString}");

            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("Connection string is null or empty.");
            }
            else
            {
                optionsBuilder.UseSqlServer(connectionString); ;
                Console.WriteLine("Connection string successfully configured.");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<City> City { get; set; }
        public DbSet<Food> Food { get; set; }
        public DbSet<Food_category> Food_category { get; set; }
        public DbSet<Restaurant> Restaurant { get; set; }
        public DbSet<Restaurant_Food> Restaurant_Food { get; set; }
       
    }
   
}