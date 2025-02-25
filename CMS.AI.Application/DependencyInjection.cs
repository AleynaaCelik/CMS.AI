using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace CMS.AI.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            // MediatR
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(assembly);
            });

            // Diğer servisler burada kaydedilebilir

            return services;
        }
    }
}