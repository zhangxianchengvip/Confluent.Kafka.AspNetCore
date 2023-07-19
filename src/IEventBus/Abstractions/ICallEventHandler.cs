using EventBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstractions
{
    public interface ICallEventHandler
    {
        Task Handle(string topic, byte[] value);
    }
}
