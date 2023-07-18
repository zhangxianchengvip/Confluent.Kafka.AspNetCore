using EventBus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IEventBus.EventBus
{
    public interface ICallHandler
    {
        Task CallHandle(string topic, byte[] value);
    }

    public class DefaultCallHandler : ICallHandler
    {
        private readonly ISubscriptionsManager _subManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceScope serviceScope;
        public DefaultCallHandler(ISubscriptionsManager subManager, IServiceScopeFactory serviceProviderFactory)
        {
            _subManager = subManager;
            this.serviceScope = serviceProviderFactory.CreateScope();
            this._serviceProvider = serviceScope.ServiceProvider;
        }

        public Task CallHandle(string topic, byte[] value)
        {
            List<Type> types = new List<Type>();
            foreach (var subscription in types)
            {
                using var scope = this._serviceProvider.CreateScope();
                IIntegrationEventHandler? handler = scope.ServiceProvider.GetService(subscription) as IIntegrationEventHandler;
                if (handler == null)
                {
                    throw new ApplicationException($"无法创建{subscription}类型的服务");
                }
                return handler.Handle(topic, value);
            }


            throw new NotImplementedException();
        }
    }
}
