using Dynamic.Json;
using EventBus;
using EventBus.AspNetCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.AspNetCore
{
    public abstract class DynamicIntegrationEventHandler : IIntegrationEventHandler
    {
        public Task Handle(string topic, byte[] value)
        {
            dynamic dynamic = DJson.Parse(value);
            return Handle(topic, dynamic);
        }

        public abstract Task Handle(string topic, dynamic value);
    }
}
