using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using KafkaSample.EventBus;
using EventBus.Abstractions;

namespace KafkaSample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IProducer<string, string> _progress;
    private readonly IEventBus _eventBus;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IEventBus bus)
    {
        _logger = logger;
        //_progress = progress;
        _eventBus = bus;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        var ss = JsonSerializer.Serialize(new Ms() { Zxc = "zxczxczxc" }, typeof(Ms));

        //await _progress.ProduceAsync("mc", new Message<string, string> { Value = ss });

      await  _eventBus.PublishAsync("Channel", new Ms() { Zxc = "zxczxczxc" });

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}