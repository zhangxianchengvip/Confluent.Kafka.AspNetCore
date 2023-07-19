using System.Threading.Tasks;

namespace EventBus.Abstractions
{
    public interface IIntegrationEventHandler
    {
        Task EventHandleAsync(string topic, byte[] value);
    }
}

