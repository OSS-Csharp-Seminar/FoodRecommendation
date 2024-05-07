using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DataAccess.Persistence;
using Core.Entiteti;
using Microsoft.AspNetCore.Hosting;

namespace MyApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Program>();
                });
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>();
        }
    }
}
