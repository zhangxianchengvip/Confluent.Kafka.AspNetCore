using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
    public interface IEventBus
    {
        void Publish<T>(string topic, T eventData);
        Task PublishAsync<T>(string topic, T eventData);
        void Subscribe(string topic, Type handlerType);
        void Unsubscribe(string topic, Type handlerType);
    }
}
