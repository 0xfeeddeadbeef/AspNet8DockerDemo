namespace AspNet8DockerDemo.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("weather")]
public sealed class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild",
        "Warm", "Balmy", "Hot", "Sweltering", "Scorching",
    ];

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get(CancellationToken cancellation = default)
    {
        await Task.Delay(TimeSpan.FromSeconds(0.6d), cancellation);

        _ = new byte[0x400000];

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
