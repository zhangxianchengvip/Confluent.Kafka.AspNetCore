using System;
using System.Collections.Generic;
using System.Linq;

namespace EventBus
{
    public static class SubscriptionsManager
    {
        private static Dictionary<string, List<Type>> _handlers = new Dictionary<string, List<Type>>();
        public static bool IsEmpty => !_handlers.Keys.Any();
        public static void Clear() => _handlers.Clear();

        /// <summary>
        /// 把eventHandlerType类型（实现了eventHandlerType接口）注册为监听了topic事件
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="eventHandlerType"></param>
        public static void AddSubscription(string topic, Type handlerType)
        {
            if (!HasSubscriptionsForEvent(topic))
            {
                _handlers.Add(topic, new List<Type>());
            }
            //如果已经注册过，则报错
            if (_handlers[topic].Contains(handlerType))
            {
                throw new ArgumentException($"Handler Type {handlerType} already registered for '{topic}'", nameof(handlerType));
            }
            _handlers[topic].Add(handlerType);
        }

        public static void RemoveSubscription(string topic, Type handlerType)
        {
            _handlers[topic].Remove(handlerType);
            if (!_handlers[topic].Any())
            {
                _handlers.Remove(topic);
            }
        }

        /// <summary>
        /// 得到名字为topic的所有监听者
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetHandlersForEvent(string topic) => _handlers[topic];

        /// <summary>
        /// 是否有类型监听topic这个事件
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        public static bool HasSubscriptionsForEvent(string topic) => _handlers.ContainsKey(topic);

    }
}
