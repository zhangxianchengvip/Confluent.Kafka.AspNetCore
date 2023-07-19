using EventBus.Abstractions;
using EventBus.SubsManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace EventBus.AspNetCore.EventBus.AspNetCore
{
    public class AspNetCoreEventBus : IEventBus
    {
        private readonly ICallEventHandler _callEventHandler;
        public AspNetCoreEventBus(ICallEventHandler callEventHandler)
        {

            _callEventHandler = callEventHandler;
        }

        public void Publish<T>(string topic, T data)
        {
            var value = JsonSerializer.Serialize(data, typeof(T));
            var channel = ChannelManager.GetChannelForTopic(topic);
            channel.Writer.TryWrite(value);
        }

        public async Task PublishAsync<T>(string topic, T data)
        {
            var value = JsonSerializer.Serialize(data, typeof(T));
            var channel = ChannelManager.GetChannelForTopic(topic);
            await channel.Writer.WriteAsync(value);
        }

        public void Subscribe(params string[] topics)
        {
            foreach (var topic in topics)
            {
                Task.Run(async () =>
                {
                    var channel = ChannelManager.GetChannelForTopic(topic);
                    while (true)
                    {
                        var item = await channel.Reader.ReadAsync();
                        await _callEventHandler.Handle(topic, Encoding.Default.GetBytes(item));
                    }
                });
            }
        }
    }
}
