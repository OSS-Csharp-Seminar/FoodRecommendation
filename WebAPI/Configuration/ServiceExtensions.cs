using Application.DataServices;
using Application.Interfaces;
using Application.Services;
using DataAccess.DataServices;
using DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Threading.RateLimiting;

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

            services.AddRateLimiter(_ =>
            {
                _.OnRejected = (context, _) =>
                {
                    if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                    {
                        context.HttpContext.Response.Headers.RetryAfter =
                            ((int)retryAfter.TotalSeconds).ToString(NumberFormatInfo.InvariantInfo);
                    }

                    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    context.HttpContext.Response.WriteAsync("Too many requests. Please try again later.");

                    return new ValueTask();
                };
                _.GlobalLimiter = PartitionedRateLimiter.CreateChained(
                    PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                    {
                        var userAgent = httpContext.Request.Headers.UserAgent.ToString();

                        return RateLimitPartition.GetFixedWindowLimiter
                        (userAgent, _ =>
                            new FixedWindowRateLimiterOptions
                            {
                                AutoReplenishment = true,
                                PermitLimit = 20,
                                Window = TimeSpan.FromSeconds(2)
                            });
                    }),
                    PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                    {
                        var userAgent = httpContext.Request.Headers.UserAgent.ToString();

                        return RateLimitPartition.GetFixedWindowLimiter
                        (userAgent, _ =>
                            new FixedWindowRateLimiterOptions
                            {
                                AutoReplenishment = true,
                                PermitLimit = 20,
                                Window = TimeSpan.FromSeconds(30)
                            });
                    }));
            });

            return services;
        }
    }
}
