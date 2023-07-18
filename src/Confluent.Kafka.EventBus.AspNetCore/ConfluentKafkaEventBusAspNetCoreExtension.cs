﻿using Confluent.Kafka.AspNetCore;
using EventBus;
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
            services.AddEventBus();
            services.AddConfluentKafkaProducer<string, string>(configuration);
            services.AddConfluentKafkaConsumer<Ignore, string>(configuration);
            return services;
        }
    }
}
