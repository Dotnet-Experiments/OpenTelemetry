using System.Net.Http.Headers;
using System.Text.Json;
using Api1.Constants;
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
    [EnableCors(ApiConstants.CorsPolicy)]
    public async Task<IEnumerable<WeatherForecast>?> Get()
    {
        _logger.LogInformation("Request started");

        var client = new HttpClient();
//        client.DefaultRequestHeaders.Add("Origin", "http://api1:8081");
        var weatherForecasts = await client.GetFromJsonAsync<IEnumerable<WeatherForecast>>("http://api2/WeatherForecast");
        return weatherForecasts;
    }
}
