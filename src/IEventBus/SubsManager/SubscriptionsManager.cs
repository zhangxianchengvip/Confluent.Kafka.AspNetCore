using System;
using System.Collections.Generic;
using System.Linq;

namespace EventBus.SubsManager
{
    public static class SubscriptionsManager
    {
        private static readonly Dictionary<string, List<Type>> Handlers = new Dictionary<string, List<Type>>();
        public static IEnumerable<string> GetTopics() => Handlers.Select(s => s.Key);
        public static bool IsEmpty => !Handlers.Keys.Any();
        public static void Clear() => Handlers.Clear();
        public static void AddSubscription(string topic, Type handlerType)
        {
            if (!HasSubscriptionsForEvent(topic))
            {
                Handlers.Add(topic, new List<Type>());
            }

            //如果已经注册过，则报错
            if (Handlers[topic].Contains(handlerType))
            {
                throw new ArgumentException($"Handler Type {handlerType} already registered for '{topic}'", nameof(handlerType));
            }

            Handlers[topic].Add(handlerType);
        }
        public static void RemoveSubscription(string topic, Type handlerType)
        {
            Handlers[topic].Remove(handlerType);
            if (!Handlers[topic].Any())
            {
                Handlers.Remove(topic);
            }
        }
        public static IEnumerable<Type> GetHandlersForEvent(string topic) => Handlers[topic];
        public static bool HasSubscriptionsForEvent(string topic) => Handlers.ContainsKey(topic);
    }
}