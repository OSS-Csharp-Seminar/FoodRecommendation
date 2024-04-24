using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DataAccess.Persistence;
using Core.Entiteti;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    // Dobavite DbContext i izvršite operacije s podacima
                    var dbContext = services.GetRequiredService<DatabaseContext>();
                    dbContext.Database.EnsureCreated();

                    // Primjer operacija s podacima
                    dbContext.City.Add(new City { Name = "City" });
                    dbContext.Restaurant.Add(new Restaurant { Name = "Restaurant" });
                    dbContext.SaveChanges();

                    Console.WriteLine("Data operations completed successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }

            host.Run();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    // Dodajte DbContext u servise
                    services.AddDbContext<DatabaseContext>();
                });
    }
}
