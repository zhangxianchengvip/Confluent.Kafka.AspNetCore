using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EventBus;
using EventBus.EventBus;
using static Confluent.Kafka.ConfigPropertyNames;

namespace Confluent.Kafka.EventBus.AspNetCore.Confluent.Kafka.EventBus
{
    public class ConfluentKafkaEventBus : IEventBus
    {

        private readonly IProducer<string, string> _producer;
        private readonly IConsumer<string, string> _consumer;
        private readonly ICallHandler _callHandler;

        public ConfluentKafkaEventBus(IProducer<string, string> producer, IConsumer<string, string> consumer, ICallHandler callHandler)
        {
            _producer = producer;
            _consumer = consumer;
            _callHandler = callHandler;
        }

        public void Publish<T>(string topic, T data)
        {
            var value = JsonSerializer.Serialize(data, typeof(T));
            _producer.Produce(topic, new Message<string, string> { Value = value });
        }

        public Task PublishAsync<T>(string topic, T data)
        {
            var value = JsonSerializer.Serialize(data, typeof(T));
            return _producer.ProduceAsync(topic, new Message<string, string> { Value = value });

        }
        public async Task Subscribe(string topic)
        {
            await Task.Run(async () =>
             {
                 using (_consumer)
                 {
                     _consumer.Subscribe(topic);
                     try
                     {
                         while (true)
                         {
                             try
                             {
                                 var cr = _consumer.Consume();
                                 await _callHandler.Handle(cr.Topic, cr.Message.Value);
                             }
                             catch (OperationCanceledException)
                             {
                                 break;
                             }
                             catch (ConsumeException e)
                             {
                                 // Consumer errors should generally be ignored (or logged) unless fatal.
                                 Console.WriteLine($"Consume error: {e.Error.Reason}");

                                 if (e.Error.IsFatal)
                                 {
                                     // https://github.com/edenhill/librdkafka/blob/master/INTRODUCTION.md#fatal-consumer-errors
                                     break;
                                 }
                             }
                             catch (Exception e)
                             {
                                 Console.WriteLine($"Unexpected error: {e}");
                                 break;
                             }
                         }
                     }
                     catch (OperationCanceledException)
                     {
                         Console.WriteLine("Closing consumer.");
                         _consumer.Close();
                     }
                 }
             });
        }
    }
}
