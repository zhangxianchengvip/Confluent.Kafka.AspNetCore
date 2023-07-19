using System;
using EventBus.Abstractions;
using System.Threading.Tasks;
using Dynamic.Json;

namespace EventBus.AspNetCore
{
    public abstract class DynamicIntegrationEventHandler : IIntegrationEventHandler
    {
        public Task EventHandleAsync(string topic, byte[] value)
        {
            try
            {
                dynamic data = DJson.Parse(value);

                return HandleAsync(topic, data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public abstract Task HandleAsync(string topic, dynamic value);
    }
}