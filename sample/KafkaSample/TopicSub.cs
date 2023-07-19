using Confluent.Kafka;
using Confluent.Kafka.AspNetCore.Confluent.Kafka.AspNetCore;

namespace KafkaSample;

public class TopicSub : BackgroundService
{
    public readonly IConsumer<Ignore, string> _consumer;

    public TopicSub(IConsumer<Ignore, string> consumer)
    {
        _consumer = consumer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        List<string> list = new List<string> { "mc" };
        await _consumer.StartConsumerLoop
        (
            (s, k) =>
            {
                Console.WriteLine($"{s}:{k}");
                return Task.FromResult(true);
            },
            list.ToArray()
        );
    }
}
