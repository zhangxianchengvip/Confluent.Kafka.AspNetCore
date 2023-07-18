using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SubscribeAttribute : Attribute
    {
        public string Topic { get; set; }
        public SubscribeAttribute(string topic)
        {
            Topic = topic;
        }
    }
}
