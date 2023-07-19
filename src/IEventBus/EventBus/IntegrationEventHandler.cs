using System;
using EventBus.Abstractions;
using System.Threading.Tasks;
using System.Text.Json;
using EventBus.Events;

namespace EventBus.AspNetCore
{
    public abstract class IntegrationEventHandler<T> : IIntegrationEventHandler where T : IntegrationEvent
    {
        public Task EventHandleAsync(string topic, byte[] value)
        {
            try
            {
                T? type = JsonSerializer.Deserialize<T>(value);
                return HandleAsync(topic, type);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public abstract Task HandleAsync(string topic, T? value);
    }
}