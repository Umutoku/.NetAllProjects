// See https://aka.ms/new-console-template for more information
using Observability.ConsoleAPP;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

Console.WriteLine("Hello, World!");



var traceProvider = Sdk.CreateTracerProviderBuilder()
    .AddSource(OpenTelemetryConstants.ActivitySourceName) 
    .ConfigureResource(cfg => cfg.AddService(OpenTelemetryConstants.ServiceName, serviceVersion: OpenTelemetryConstants.ServiceVersion)
    .AddAttributes(new List<KeyValuePair<string, object>>()
    { new("host.machineName", Environment.MachineName),
      new("host.environment", "dev"),
      
    }
    )).AddConsoleExporter().Build();
   
var serviceHelper = new ServiceHelper();
await serviceHelper.Work1();