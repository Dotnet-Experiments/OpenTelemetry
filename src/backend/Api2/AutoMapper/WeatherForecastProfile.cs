using Api2.Entities;
using AutoMapper;

namespace Api2.AutoMapper;

public class WeatherForecastProfile : Profile
{
    public WeatherForecastProfile()
    {
        CreateMap<WeatherForecast, Models.WeatherForecast>();
    }
    
}