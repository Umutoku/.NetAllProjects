// See https://aka.ms/new-console-template for more information
using Observability.ConsoleAPP;
using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Diagnostics;

Console.WriteLine("Hello, World!");

ActivitySource.AddActivityListener(new ActivityListener
{
    ShouldListenTo = (activitySource) => activitySource.Name == OpenTelemetryConstants.ActivitySourceName,
    ActivityStarted = (activity) =>
    {
        Console.WriteLine($"Activity başladı: {activity.OperationName}");
    },
    ActivityStopped = (activity) =>
    {
        Console.WriteLine($"Activity bitti: {activity.OperationName}");
    }
});

using var traceProviderFile = Sdk.CreateTracerProviderBuilder()
    .AddSource(OpenTelemetryConstants.ActivitySourceFileName).Build();




using var traceProvider = Sdk.CreateTracerProviderBuilder()
    .AddSource(OpenTelemetryConstants.ActivitySourceName) 
    .ConfigureResource(cfg => cfg.AddService(OpenTelemetryConstants.ServiceName, serviceVersion: OpenTelemetryConstants.ServiceVersion)
    .AddAttributes(new List<KeyValuePair<string, object>>()
    { new("host.machineName", Environment.MachineName),
      new("host.environment", "dev"),
      
    }
    )).AddConsoleExporter()
    .AddZipkinExporter(zopt=>zopt.Endpoint= new Uri("http://localhost:9411/api/v2/spans")) // Zipkin
    .AddOtlpExporter() // OpenTelemetry Collector
    .Build();
   
var serviceHelper = new ServiceHelper();
await serviceHelper.Work1();