using Api2;
using Api2.Entities;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Enrichers.Span;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithSpan()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Open telemetry
builder.Services.AddOpenTelemetryTracing(b =>
{
    b.AddConsoleExporter();
    b.AddOtlpExporter(options =>
    {
        options.Endpoint = new Uri("http://agent:4319/v1/traces");
        options.Protocol = OtlpExportProtocol.Grpc;
    });

    //b.AddJaegerExporter(opt => opt.Endpoint = new Uri("http://agent:4318/v1/traces"));
    
    // TODO: receive traces from our own custom sources
    //b.AddSource(TelemetryConstants.MyAppTraceSource);
    
    // decorate our service name so we can find it when we look inside Jaeger
    b.SetResourceBuilder(ResourceBuilder.CreateDefault()
        .AddService("Api2", "Demo"));
    
    // receive traces from built-in sources
    b.AddAspNetCoreInstrumentation();
    b.AddHttpClientInstrumentation();
    b.AddSqlClientInstrumentation();
});

builder.Services.AddDbContext<WeatherDbContext>(options => 
    options.UseSqlServer("Server=sqlserver;Database=TestDb;User Id=sa; Password=Test@12345"));            
builder.Services.AddAutoMapper(typeof(Program)); 

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// GET http://192.168.64.6/WeatherForecast
// Traceparent: 00-0af7651916cd43dd8448eb211c80319c-b7ad6b7169203331-01
// Traceparent: 00-8652a752089f33e2659dff28d683a18f-7359b90f4355cfd9-01
