using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EventBus;
using EventBus.EventBus;

namespace Confluent.Kafka.EventBus.AspNetCore.Confluent.Kafka.EventBus
{
    public class ConfluentKafkaEventBus : IEventBus
    {

        private readonly IProducer<string, byte[]> _producer;
        private readonly IConsumer<string, byte[]> _consumer;
        private readonly ICallHandler _callHandler;

        public ConfluentKafkaEventBus(IProducer<string, byte[]> producer, IConsumer<string, byte[]> consumer, ICallHandler callHandler)
        {
            _producer = producer;
            _consumer = consumer;
            _callHandler = callHandler;
        }

        public void Publish<T>(string topic, T eventData)
        {

            throw new NotImplementedException();
        }

        public Task PublishAsync<T>(string topic, T eventData)
        {
            return _producer.ProduceAsync(topic, new Message<string, byte[]> { });

        }

        public void Subscribe(string topic, Type handlerType)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe(string topic, Type handlerType)
        {
            throw new NotImplementedException();
        }
    }
}
