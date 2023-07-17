using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Confluent.Kafka.AspNetCore.Confluent.Kafka.AspNetCore
{
    public static class ConsumerExtension
    {
        public async static Task StartConsumerLoop<TKey, TValue>(this IConsumer<TKey, TValue> consumer, Func<string, TValue, Task<bool>> handler, params string[] topics)
        {
            await Task.Run(async () =>
             {
                 using (consumer)
                 {
                     consumer.Subscribe(topics);
                     try
                     {
                         while (true)
                         {
                             try
                             {
                                 var cr = consumer.Consume();
                                 await handler(cr.Topic, cr.Message.Value);
                             }
                             catch (OperationCanceledException)
                             {
                                 break;
                             }
                             catch (ConsumeException e)
                             {
                                 // Consumer errors should generally be ignored (or logged) unless fatal.
                                 Console.WriteLine($"Consume error: {e.Error.Reason}");

                                 if (e.Error.IsFatal)
                                 {
                                     // https://github.com/edenhill/librdkafka/blob/master/INTRODUCTION.md#fatal-consumer-errors
                                     break;
                                 }
                             }
                             catch (Exception e)
                             {
                                 Console.WriteLine($"Unexpected error: {e}");
                                 break;
                             }
                         }
                     }
                     catch (OperationCanceledException)
                     {
                         Console.WriteLine("Closing consumer.");
                         consumer.Close();
                     }
                 }
             });
        }
    }
}
