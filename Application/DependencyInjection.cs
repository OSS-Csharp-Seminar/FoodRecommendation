using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;
using FluentValidation;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            // Poboljšanje postojećih funkcionalnosti
            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssemblies(assembly));

            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }

}
