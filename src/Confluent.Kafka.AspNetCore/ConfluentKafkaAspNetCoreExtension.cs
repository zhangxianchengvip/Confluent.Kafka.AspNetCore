using Auto.Options;
using Confluent.Kafka.AspNetCore.Confluent.Kafka.AspNetCore;
using Fc.Bus.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Confluent.Kafka.AspNetCore
{
    public static class ConfluentKafkaAspNetCoreExtension
    {
        public static IServiceCollection AddConfluentKafkaProducer<TKey, TValue>(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.GetOptions<ConfluentKafkaOptions>();

            services.AddSingleton(sp =>
            {

                return new DefaultKafkaPersistentConnection(options).CreateProducer<TKey, TValue>();
            });

            return services;
        }

        public static IServiceCollection AddConfluentKafkaConsumer<TKey, TValue>(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.GetOptions<ConfluentKafkaOptions>();

            services.AddScoped(sp =>
            {
                return new DefaultKafkaConsumerConnection(options).CreateConsumer<TKey, TValue>();
            });

            return services;
        }
    }
}
