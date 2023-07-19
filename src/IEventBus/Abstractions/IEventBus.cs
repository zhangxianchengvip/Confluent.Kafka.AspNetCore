using System.Threading.Tasks;

namespace EventBus.Abstractions
{
    public interface IEventBus
    {
        void Publish<T>(string topic, T data);
        Task PublishAsync<T>(string topic, T data);
        void Subscribe(params string[] topics);
    }
}