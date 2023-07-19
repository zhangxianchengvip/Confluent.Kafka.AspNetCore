using EventBus;
using EventBus.Abstractions;
using EventBus.AspNetCore;
using EventBus.Events;
using EventBus.SubsManager;

namespace KafkaSample.EventBus;

[Subscribe("Channel")]
public class MsgHandler : DynamicIntegrationEventHandler
{
    public async override Task HandleAsync(string topic, dynamic value)
    {
        string values = value.Zxc;
        Console.WriteLine(values);
        await Task.Yield();
    }
}

public class Ms
{
    public string Zxc { get; set; }
}