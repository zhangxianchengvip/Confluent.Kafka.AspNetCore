using System;
using Dynamic.Json;
using System.Threading.Tasks;

namespace EventBus.AspNetCore
{
    public abstract class DynamicIntegrationEventHandler : IIntegrationEventHandler
    {
        public Task BaseHandle(string topic, Byte[] value)
        {
            dynamic dynamic = DJson.Parse(value);
            return Handle(topic, dynamic);
        }

        public abstract Task Handle(string topic, dynamic value);
    }
}
