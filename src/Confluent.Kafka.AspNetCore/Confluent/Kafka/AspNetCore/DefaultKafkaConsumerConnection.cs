using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Confluent.Kafka.AspNetCore.Confluent.Kafka.AspNetCore
{
    public class DefaultKafkaConsumerConnection
    {
        private readonly ConfluentKafkaOptions _options;

        public DefaultKafkaConsumerConnection(ConfluentKafkaOptions options)
        {
            _options = options;
        }

        public IConsumer<TKey, TValue> CreateConsumer<TKey, TValue>()
        {
            var config = new ConsumerConfig
            {
                GroupId = _options.GroupId,
                BootstrapServers = _options.BootstrapServers,
                //EnableAutoCommit = false, // 禁止AutoCommit
                //Acks = Acks.Leader, // 假设只需要Leader响应即可
                AutoOffsetReset = AutoOffsetReset.Earliest,// 从最早的开始消费起
                AllowAutoCreateTopics = true
            };
            return new ConsumerBuilder<TKey, TValue>(config).Build();

        }
    }
}
