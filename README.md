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
builder.Services.ConfluentKafka();
```

3. 构造函数调用

```C#
private readonly IProducer<> _producer;
public WeatherForecastController(IProducer<> producer)
{
  _producer = producer;
}
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

