using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace CMS.AI.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            return services;
        }
    }
}