using EventBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.EventBus
{
    public interface ICallHandler
    {
        Task Handle(string topic, byte[] value);
    }
}
