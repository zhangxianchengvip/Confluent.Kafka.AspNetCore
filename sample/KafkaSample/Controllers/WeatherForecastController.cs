using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;

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
    private readonly IProducer<string, byte[]> _progress;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IProducer<string, byte[]> progress)
    {
        _logger = logger;
        _progress = progress;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        await _progress.ProduceAsync("", new Message<string, byte[]> { });
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
