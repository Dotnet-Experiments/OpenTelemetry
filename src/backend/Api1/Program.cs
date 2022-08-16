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
        .AddService("Api1", "Demo"));
    
    // receive traces from built-in sources
    b.AddAspNetCoreInstrumentation();
    b.AddHttpClientInstrumentation();
    b.AddSqlClientInstrumentation();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy1",
        policy =>
        {
            policy.WithOrigins("http://localhost");
        });
});

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

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();

// GET http://localhost:8081/WeatherForecast
// Traceparent: 00-0af7651916cd43dd8448eb211c80319c-b7ad6b7169203331-01
