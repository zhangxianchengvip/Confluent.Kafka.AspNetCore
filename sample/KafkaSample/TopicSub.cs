using Confluent.Kafka;
using Confluent.Kafka.AspNetCore.Confluent.Kafka.AspNetCore;

namespace KafkaSample;

public class TopicSub : BackgroundService
{
    public IServiceProvider Services { get; }
    public TopicSub(IServiceProvider services)
    {
        Services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        List<string> list = new List<string> { "mc" };
        using (var scope = Services.CreateScope())
        {
            foreach (var item in list)
            {
                var _consumer = scope.ServiceProvider.GetRequiredService<IConsumer<Ignore, string>>();

                await _consumer.StartConsumerLoop
                (
                    (s, k) =>
                    {
                        Console.WriteLine($"{s}:{k}");
                        return Task.FromResult(true);
                    }, item
                );
            }
        }
    }


}
