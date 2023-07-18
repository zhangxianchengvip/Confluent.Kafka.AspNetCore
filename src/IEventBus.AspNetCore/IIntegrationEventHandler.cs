using System;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
namespace EventBus.AspNetCore
{
    public abstract class IIntegrationEventHandler<T> : IIntegrationEventHandler
    {
        public Task Handle(string topic, byte[] value)
        {
            T type = JsonSerializer.Deserialize<T>(value);
            return Handle(topic, type);
        }
        public abstract Task Handle(string topic, T value);
    }
}
