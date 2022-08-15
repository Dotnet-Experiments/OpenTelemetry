using Api2.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api2;

public class WeatherDbContext : DbContext
{
    private readonly ILoggerFactory _loggerFactory;

    public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    
    public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options)
    {
        _loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<WeatherForecast>().ToTable("Customer");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseLoggerFactory(_loggerFactory);
    }
    
}