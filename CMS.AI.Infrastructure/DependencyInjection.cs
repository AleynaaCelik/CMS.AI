using CMS.AI.Application.Common.Interfaces;
using CMS.AI.Domain.Entities;
using CMS.AI.Infrastructure.Persistance;
using CMS.AI.Infrastructure.Repositories;
using CMS.AI.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CMS.AI.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
             this IServiceCollection services,
             IConfiguration configuration)
        {
            // Database
            services.AddDbContext<CMS.AI.Infrastructure.Persistance.ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(CMS.AI.Infrastructure.Persistance.ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider =>
                (IApplicationDbContext)provider.GetRequiredService<CMS.AI.Infrastructure.Persistance.ApplicationDbContext>());

            // Redis Cache
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
                options.InstanceName = "CMS.AI:";
            });
            services.AddSingleton<ICacheService, RedisCacheService>();

            // HttpClient Factory
            services.AddHttpClient();

            // OpenAI integration
            services.AddScoped<IAIService, OpenAIService>();

            // Elasticsearch integration
            services.AddScoped<ISearchService<Content>, ElasticsearchService>();

            // Repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // Core services
            services.AddScoped<IDateTime, DateTimeService>();

            return services;
        }
    }
}