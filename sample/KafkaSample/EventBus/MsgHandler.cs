using EventBus;
using EventBus.AspNetCore;

namespace KafkaSample.EventBus;

[Subscribe("mc")]
public class MsgHandler : IIntegrationEventHandler<string>
{
    public override Task Handle(string topic, string value)
    {
        Console.WriteLine($"{topic}:{value}");
        return Task.CompletedTask;
    }
}

public class Ms
{
    public string Zxc { get; set; }
}