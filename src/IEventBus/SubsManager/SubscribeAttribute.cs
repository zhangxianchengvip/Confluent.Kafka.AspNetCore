using System;

namespace EventBus.SubsManager
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SubscribeAttribute : Attribute
    {
        public string Topic { get; set; }
        public SubscribeAttribute(string topic)
        {
            Topic = topic;
        }
    }
}
