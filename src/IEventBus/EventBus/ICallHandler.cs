using EventBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.EventBus
{
    public interface ICallHandler
    {
        Task Handle(string topic, byte[] value);
    }

    public class DefaultCallHandler : ICallHandler
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceScope serviceScope;
        public DefaultCallHandler(IServiceScopeFactory serviceProviderFactory)
        {
            this.serviceScope = serviceProviderFactory.CreateScope();
            this._serviceProvider = serviceScope.ServiceProvider;
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
                    return handler.BaseHandle(topic, value);
                }
            }
            else
            {
                string entryAsm = Assembly.GetEntryAssembly().GetName().Name;
                Debug.WriteLine($"找不到可以处理eventName={topic}的处理程序，entryAsm:{entryAsm}");
            }
            return Task.CompletedTask;
        }
    }
}
