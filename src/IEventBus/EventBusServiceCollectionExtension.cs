using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EventBus.Abstractions;
using EventBus.SubsManager;

namespace EventBus
{
    public static class EventBusServiceCollectionExtension
    {
        public static IApplicationBuilder UseEventBus(this IApplicationBuilder app)
        {
            var sp = app.ApplicationServices;
            var bus = sp.GetRequiredService<IEventBus>();
            bus.Subscribe(SubscriptionsManager.GetTopics().ToArray());
            return app;
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where
            (
                s =>
                    !s.FullName.Contains("System") &&
                    !s.FullName.Contains("Microsoft") &&
                    !s.FullName.Contains("netstandard") &&
                    !s.FullName.Contains("Swashbuckle")
            ).ToArray();

            AddEventBus(services, assemblies);

            return services;
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services, params Assembly[] assemblies)
        {
            List<Type> handlers = new List<Type>();

            foreach (var asm in assemblies)
            {
                //用GetTypes()，这样非public类也能注册
                var types = asm.GetTypes().Where
                (
                    t => t.IsAbstract == false && t.GetCustomAttribute<SubscribeAttribute>() != null
                );

                handlers.AddRange(types);
            }

            return AddEventBus(services, handlers);
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services, List<Type> types)
        {
            services.AddSingleton<ICallEventHandler, DefaultCallEventHandler>();

            foreach (var type in types)
            {
                services.AddScoped(type, type);

                var subscribes = type.GetCustomAttributes<SubscribeAttribute>();

                if (subscribes.Any() == false)
                {
                    throw new ApplicationException($"There shoule be at least one EventNameAttribute on {type}");
                }

                foreach (var subscribe in subscribes)
                {
                    SubscriptionsManager.AddSubscription(subscribe.Topic, type);
                }
            }

            return services;
        }
    }
}