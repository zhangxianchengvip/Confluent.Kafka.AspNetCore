using EventBus.Abstractions;
using EventBus.SubsManager;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace EventBus
{
    public class DefaultCallEventHandler : ICallEventHandler
    {
        private readonly IServiceProvider _serviceProvider;

        public DefaultCallEventHandler(IServiceScopeFactory serviceProviderFactory)
        {
            var serviceScope = serviceProviderFactory.CreateScope();
            _serviceProvider = serviceScope.ServiceProvider;
        }

        public Task Handle(string topic, byte[] value)
        {
            if (SubscriptionsManager.HasSubscriptionsForEvent(topic))
            {
                var subscriptions = SubscriptionsManager.GetHandlersForEvent(topic);

                foreach (var subscription in subscriptions)
                {
                    using var scope = this._serviceProvider.CreateScope();

                    IIntegrationEventHandler? handler = scope.ServiceProvider.GetService(subscription) as IIntegrationEventHandler;

                    if (handler == null)
                    {
                        throw new ApplicationException($"无法创建{subscription}类型的服务");
                    }

                    return handler.EventHandleAsync(topic, value);
                }
            }
            else
            {
                string? entryAsm = Assembly.GetEntryAssembly()?.GetName().Name;

                Debug.WriteLine($"找不到可以处理eventName={topic}的处理程序，entryAsm:{entryAsm}");
            }

            return Task.CompletedTask;
        }
    }
}