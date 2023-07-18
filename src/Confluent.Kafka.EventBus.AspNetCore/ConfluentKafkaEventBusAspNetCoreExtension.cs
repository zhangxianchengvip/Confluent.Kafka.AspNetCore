using Confluent.Kafka.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Confluent.Kafka.EventBus.AspNetCore
{
    public static class ConfluentKafkaEventBusAspNetCoreExtension
    {

        public static IServiceCollection AddConfluentKafkaEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddConfluentKafkaProducer<string, byte[]>(configuration);
            services.AddConfluentKafkaConsumer<Ignore, byte[]>(configuration);
            return services;
        }
    }
}
