using EventBus;
using EventBus.EventBus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace IEventBus.AspNetCore
{
    public static class EventAspNetCoreExtension
    {
        public static IServiceCollection AddEventBusAspNetCore(this IServiceCollection services)
        {
            services.AddSingleton<ICallHandler, DefaultCallHandler>();
            services.AddEventBus();
            return services;
        }
    }
}
