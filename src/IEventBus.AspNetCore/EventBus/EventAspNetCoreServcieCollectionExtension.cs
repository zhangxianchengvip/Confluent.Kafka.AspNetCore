using EventBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace EventBus.AspNetCore
{
    public static class EventAspNetCoreServcieCollectionExtension
    {
        public static IServiceCollection AddEventBusAspNetCore(this IServiceCollection services)
        {
            services.AddSingleton<ICallEventHandler, DefaultCallEventHandler>();
            services.AddEventBus();
            return services;
        }
    }
}
