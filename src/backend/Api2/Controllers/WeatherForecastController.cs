using Api2;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using Models = Api2.Models;
using Entities = Api2.Entities;

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
    private readonly WeatherDbContext _dbContext;
    private readonly IMapper _mapper;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherDbContext dbContext, IMapper mapper)
    {
        _logger = logger;
        _dbContext = dbContext;
        _mapper = mapper;

        _logger.LogInformation("v. 6 - The god of the day is {@God}", "Odin");
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<Models.WeatherForecast> Get()
    {
        _logger.LogInformation("Request started");

        var results = _dbContext.WeatherForecasts.ToList();
        return _mapper.Map<IEnumerable<Models.WeatherForecast>>(results);
        
        // return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        // {
        //     Date = DateTime.Now.AddDays(index),
        //     TemperatureC = Random.Shared.Next(-20, 55),
        //     Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        // })
        // .ToArray();
    }
}
