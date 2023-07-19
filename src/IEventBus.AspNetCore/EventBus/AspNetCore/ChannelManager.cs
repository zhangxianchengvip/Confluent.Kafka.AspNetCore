using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Channels;

namespace EventBus.AspNetCore.EventBus.AspNetCore
{
    public static class ChannelManager
    {
        public static readonly Dictionary<string, Channel<string>> Channels = new Dictionary<string, Channel<string>>();

        public static Channel<string> GetChannelForTopic(string topic) => Channels[topic];
        public static void AddChannel(string topic)
        {
            if (!HasChannelForTopic(topic))
            {
                Channels.Add(topic, Channel.CreateUnbounded<string>());
            }
        }
        public static bool HasChannelForTopic(string topic) => Channels.ContainsKey(topic);
    }
}
