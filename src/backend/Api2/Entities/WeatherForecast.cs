using System.ComponentModel.DataAnnotations;

namespace Api2.Entities;

public class WeatherForecast
{
    [Required]
    [Key]
    public int Id { get; set; }
    [Required]
    public DateTime Date { get; set; }

    [Required]
    public int TemperatureC { get; set; }

}