using Confluent.Kafka;
using Confluent.Kafka.AspNetCore;
using System.Collections.Generic;


namespace Fc.Bus.Kafka
{
    public class DefaultKafkaPersistentConnection
    {
        private readonly ConfluentKafkaOptions _options;

        public DefaultKafkaPersistentConnection(ConfluentKafkaOptions options)
        {
            _options = options;
        }
        public IProducer<TKey, TValue> CreateProducer<TKey, TValue>()
        {
            var config = new ProducerConfig(new Dictionary<string, string>())
            {
                BootstrapServers = _options.BootstrapServers,
                QueueBufferingMaxMessages = _options.QueueBufferingMaxMessages,
                MessageTimeoutMs = _options.MessageTimeoutMs,
                RequestTimeoutMs = _options.RequestTimeoutMs
            };

            return new ProducerBuilder<TKey, TValue>(config).Build();
        }

        public void Dispose()
        {
            this.Dispose();
        }
    }
}
