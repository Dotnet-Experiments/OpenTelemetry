[toc]

# OpenTelemetry


## OpenTelemetry integration in .NET Projects

- Install nuget packages:

  - OpenTelemetry.Api
  - OpenTelemetry.Exporter.OpenTelemetryProtocol
  - OpenTelemetry.Extensions.Hosting
  - OpenTelemetry.Instrumentation.AspNetCore
  - OpenTelemetry.Instrumentation.Http
  - OpenTelemetry.Instrumentation.SqlClient

- Setup OpenTelemetry in `Program.cs`:

  ```cs
  builder.Services.AddOpenTelemetryTracing(b =>
  {
      b.AddOtlpExporter(options => options.Endpoint = new Uri("http://agent:4318/v1/traces"));
      
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
  ```

- 

## Serilog structured logging configuration for Loki

- install nuget packages:
  - Serilog.AspNetCore
  - Serilog.Sinks.Grafana.Loki

- add the following to `Program.cs`, just below the builder creation:
```cs
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
```

- add these settings to appsettings.Development.json:
```json
  "Serilog": {
    "Using": [ "Serilog.Sinks.File"],
    "MinimumLevel": {
      "Default": "Information"
    },
    "WriteTo": [
      {
        "Name": "File",
        "Args": {
          "path": "/tmp/OpenTelemetryTest/api1-.log",
          "rollingInterval": "Day",
          "outputTemplate": "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {TraceId} {CorrelationId} {Level:u3}] {Message:lj}{NewLine}{Exception}"
        }
      },

      {
        "Name": "GrafanaLoki",
        "Args": {
          "uri": "http://loki:3100",
          "labels": [
            {
              "key": "app",
              "value": "Api1"
            }
          ],
          "propertiesAsLabels": [
            "app"
          ]
        }
      }      
    ],
    
  }
```

- test that we get logs in Grafana (filter by app label)
  Go to http://localhost:3000, log in as admin/admin and navigate to Explore -> Loki:
  
  ![image-20220805000004153](images/image-20220805000004153.png)
  
- enrich logging with TraceId and SpanId
  - install `Serilog.Enrichers.Span` nuget package
  - add `.Enrich.WithSpan()` to Serilog's `LoggerConfiguration` (Program.cs)


## Loki filtering

- Add Json filter
- Add line format: `[{{.level}}] {{.Message}}`
- 
