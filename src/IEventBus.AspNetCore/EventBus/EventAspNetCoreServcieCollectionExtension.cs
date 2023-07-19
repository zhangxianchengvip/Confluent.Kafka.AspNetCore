using EventBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace EventBus.AspNetCore
{
    public static class EventAspNetCoreServcieCollectionExtension
    {
        public static IServiceCollection AddEventBusAspNetCore(this IServiceCollection services)
        {
            services.AddEventBus();
            return services;
        }
    }
}
