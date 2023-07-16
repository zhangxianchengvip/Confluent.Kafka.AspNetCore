using Auto.Options;
using Fc.Bus.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Confluent.Kafka.AspNetCore
{
    public static class ConfluentKafkaAspNetCoreExtension
    {
        public static IServiceCollection AddKafka(this IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {
                var options = sp.GetOptions<KafkaOptions>();

                return new DefaultKafkaPersistentConnection(options).CreateProducer();
            });

            return services;

        }
    }
}
