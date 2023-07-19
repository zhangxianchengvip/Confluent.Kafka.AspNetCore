using System;
using System.Text.Json;
using System.Threading.Tasks;
using EventBus.Abstractions;

namespace Confluent.Kafka.EventBus.AspNetCore.Confluent.Kafka.EventBus
{
    public class ConfluentKafkaEventBus : IEventBus
    {
        private readonly IProducer<string, string> _producer;
        private readonly IConsumer<Ignore, byte[]> _consumer;
        private readonly ICallEventHandler _callEventHandler;

        public ConfluentKafkaEventBus(IProducer<string, string> producer, IConsumer<Ignore, byte[]> consumer, ICallEventHandler callEventHandler)
        {
            _producer = producer;
            _consumer = consumer;
            _callEventHandler = callEventHandler;
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

        public void Subscribe(params string[] topics)
        {
            Task.Run(async () => { await StartConsumerLoop(topics); });
        }
        private async Task StartConsumerLoop(params string[] topics)
        {
            using (_consumer)
            {
                _consumer.Subscribe(topics);
                try
                {
                    while (true)
                    {
                        try
                        {
                            var cr = _consumer.Consume();
                            await _callEventHandler.Handle(cr.Topic, cr.Message.Value);
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
        }
    }
}