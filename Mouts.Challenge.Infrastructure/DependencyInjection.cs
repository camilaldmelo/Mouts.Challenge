using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mouts.Challenge.Application.Common.Interfaces;
using Mouts.Challenge.Application.Sales.Interfaces;
using Mouts.Challenge.Domain.Entities;
using Mouts.Challenge.Infrastructure.Persistence;
using Mouts.Challenge.Infrastructure.Persistence.Repositories;
using Mouts.Challenge.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Mouts.Challenge.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString =
            configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString)));

            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEventPublisher, EventPublisher>();

            return services;
        }
    }
}
