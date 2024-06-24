using Application.DataServices;
using Application.Interfaces;
using Application.Services;
using DataAccess.DataServices;
using DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Add application services
            services.AddScoped<ICityLogic, CityLogic>();
            services.AddScoped<ICityData, CityData>();

            services.AddScoped<IRestaurantLogic, RestaurantLogic>();
            services.AddScoped<IRestaurantDS, RestaurantDS>();

            services.AddScoped<IFoodLogic, FoodLogic>();
            services.AddScoped<IFoodData, FoodData>();

            services.AddScoped<IFoodCategoryLogic, FoodCategoryLogic>();
            services.AddScoped<IFoodCategoryData, FoodCategoryData>();

            services.AddScoped<IRestaurantFoodLogic, RestaurantFoodLogic>();
            services.AddScoped<IRestaurantFoodData, RestaurantFoodData>();



            return services;

        }
    }
}
