using CMS.AI.Application.Common.Interfaces;
using CMS.AI.Infrastructure.Persistance;
using CMS.AI.Infrastructure.Repositories;
using CMS.AI.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CMS.AI.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider =>
                provider.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Registry for basic services
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IDateTime, DateTimeService>();

            return services;
        }
    }

}
