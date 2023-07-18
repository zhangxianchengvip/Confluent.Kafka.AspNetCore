using EventBus.EventBus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
    public interface IEventBus
    {

        void Publish<T>(string topic, T data);
        Task PublishAsync<T>(string topic, T data);
        Task Subscribe(string topic);
    }
}
