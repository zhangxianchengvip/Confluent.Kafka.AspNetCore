using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Confluent.Kafka.AspNetCore.Extensions
{
    public interface ISubscribeHandler<TValue>
    {
        Task Handle(TValue value);
    }
}

