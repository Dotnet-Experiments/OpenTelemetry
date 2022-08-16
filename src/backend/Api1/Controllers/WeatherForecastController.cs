using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Api1.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;

        _logger.LogInformation("v. 6 - The god of the day is {@God}", "Odin");
    }

    [HttpGet(Name = "GetWeatherForecast")]
    [EnableCors("Policy1")]
    public async Task<IEnumerable<WeatherForecast>?> Get()
    {
        _logger.LogInformation("Request started");

        // TODO: call via a Rest client that exposes the model from Api2

        var client = new HttpClient();
        //client.DefaultRequestHeaders.Accept.Clear();
        //client.DefaultRequestHeaders.Accept.Add(
        //    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        //client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

        var result = await client.GetAsync("http://api2/WeatherForecast");

        var weatherForecasts = await JsonSerializer.DeserializeAsync<IEnumerable<WeatherForecast>>(await result.Content.ReadAsStreamAsync());

        return weatherForecasts;

    }
}
