using Auto.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Confluent.Kafka.AspNetCore
{
    [AutoOptions]
    public class ConfluentKafkaOptions
    {
        private const string DefaultBootstrapServers = "localhost:9092";

        public string BootstrapServers { get; set; } = DefaultBootstrapServers;

        public string GroupId { get; set; } = typeof(ConfluentKafkaOptions).Assembly.FullName;

        public int QueueBufferingMaxMessages { get; set; } = 10;

        public int MessageTimeoutMs { get; set; } = 5000;

        public int RequestTimeoutMs { get; set; } = 3000;
    }

}
