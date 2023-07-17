# Confluent.Kafka.AspNetCore   

#### 介绍
#### Confluent.Kafka Asp.Net Core   服务注册扩展

1. 安装

- [Package Manager](https://www.nuget.org/packages/FreeRedis.AspNetCore)

```
Install-Package Confluent.Kafka.AspNetCore
```

- [.NET CLI](https://www.nuget.org/packages/FreeRedis.AspNetCore)

```
dotnet add package Confluent.Kafka.AspNetCore
```

2. 注册Confluent.Kafka

```c#
// 生产者
builder.Services.AddConfluentKafkaProducer<string, byte[]>(builder.Configuration);
//Or 消费者
builder.Services.AddConfluentKafkaConsumer<Ignore, string>(builder.Configuration);
```

3. 生产者构造函数注入及使用

```C#
//注入
private readonly IProducer<string, byte[]> _progress;
public WeatherForecastController(IProducer<string, byte[]> progress)
{
  _producer = producer;
}
//使用
[HttpGet(Name = "GetWeatherForecast")]
public async Task<IEnumerable<WeatherForecast>> Get()
{
    var value = Encoding.UTF8.GetBytes("zxc");

    await _progress.ProduceAsync("mc", new Message<string, byte[]> { Key = "zxc", Value = value });

    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    {
       Date = DateTime.Now.AddDays(index),
       TemperatureC = Random.Shared.Next(-20, 55),
       Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    }).ToArray();
}
```

4.消费者使用

```C#
//继承BackgroundService类覆写ExecuteAsync 订阅topic
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
//注入后台服务
builder.Services.AddHostedService<TopicSub>();
```





4.配置文件

```json
{
 "ConfluentKafka": {
      "BootstrapServers":"",
       "GroupId":"",
       "QueueBufferingMaxMessages":10,
       "MessageTimeoutMs": 5000,
      "RequestTimeoutMs": 3000
  }
}
```

