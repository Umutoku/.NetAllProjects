using MassTransit.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTelemetry.Shared
{
    public static class OpenTelemetryExtensions
    {
        public static void AddOpenTelemetryExt(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<OpenTelemetryConstants>(configuration.GetSection("OpenTelemetry"));
            var openTelemetryConstants = (configuration.GetSection("OpenTelemetry").Get<OpenTelemetryConstants>())!;

            ActivitySourceProvider.Source = new ActivitySource(openTelemetryConstants.ActivitySourceName);

            services.AddOpenTelemetry().WithTracing(options =>
            {

                options.AddSource(openTelemetryConstants.ActivitySourceName)
                .AddSource(DiagnosticHeaders.DefaultListenerName) // Bu sayede diagnostic headers'ı trace içerisinde görebiliriz.
                   .ConfigureResource(resources =>
                   {
                       resources.AddService(serviceName: openTelemetryConstants.ServiceName, serviceVersion: openTelemetryConstants.ServiceVersion);
                   }
                   );
                options.AddAspNetCoreInstrumentation(aspnetCoreOptions =>
                {
                    aspnetCoreOptions.Filter = (httpContext) =>
                    {
                        if (!string.IsNullOrEmpty(httpContext.Request.Path.Value))
                            return httpContext.Request.Path.Value.Contains("api", StringComparison.InvariantCulture);

                        return false;
                    };
                    aspnetCoreOptions.RecordException = true; // record exception sayesinde hata oluştuğunda trace içerisinde stacktrace bilgisi de yer alır.

                    aspnetCoreOptions.EnrichWithException = (activity, exception) =>
                    {
                        activity.SetTag("exception.message", exception.Message); // hata mesajı
                        activity.SetTag("exception.stacktrace", exception.StackTrace); // hata stacktrace'i
                    };
                });
                options.AddEntityFrameworkCoreInstrumentation(efCore =>
                {
                    efCore.SetDbStatementForText = true; // sql sorgularını trace içerisinde görmek için
                    efCore.SetDbStatementForStoredProcedure = true; // stored procedure çağrılarını trace içerisinde görmek için
                    efCore.EnrichWithIDbCommand = (activity, dbCommand) => // Enrich sayesinde trace içerisinde sql sorgularının parametreleri de yer alır.
                      {
                         activity.SetTag("db.command", dbCommand.CommandText); // sql sorgusu
                          activity.SetTag("db.command_type", dbCommand.CommandType.ToString()); // sql sorgu tipi
                         
                      };
                });
                options.AddHttpClientInstrumentation(httpOptions =>
                {
                    httpOptions.FilterHttpRequestMessage = (request) => // http isteklerini trace içerisinde görmek için
                    {
                        if (!string.IsNullOrEmpty(request.RequestUri?.AbsolutePath))
                            return !request.RequestUri.AbsolutePath.Contains("http://localhost:9200", StringComparison.InvariantCulture);

                        return true;
                    };

                    httpOptions.EnrichWithHttpRequestMessage = (activity, request) =>
                    {
                        activity.SetTag("http.url", request.RequestUri?.ToString()); // http isteği
                        activity.SetTag("http.method", request.Method?.ToString()); // http isteği tipi
                        activity.SetTag("http.host", request.RequestUri?.Host); // http isteği host bilgisi
                        activity.SetTag("http.path", request.RequestUri?.AbsolutePath); // http isteği path bilgisi
                        activity.SetTag("http.body", request.Content?.ReadAsStringAsync()); // http isteği içeriği
                    };

                    httpOptions.EnrichWithHttpResponseMessage = (activity, response) =>
                    {
                        activity.SetTag("http.status_code", response.StatusCode.ToString()); // http isteği sonucu
                        activity.SetTag("http.status_text", response.ReasonPhrase); // http isteği sonucu açıklaması
                        activity.SetTag("http.body", response.Content?.ReadAsStringAsync()); // http isteği sonucu içeriği   
                    };

                }
                ); // httpclient ile yapılan isteklerin trace içerisinde görmek için

                options.AddRedisInstrumentation(redisOptions =>
                {
                    redisOptions.SetVerboseDatabaseStatements = true; // redis sorgularını trace içerisinde görmek için
                    redisOptions.Enrich = (activity, command) =>
                    {
                        activity.SetTag("redis.command", command.Command); // redis sorgusu
                        activity.SetTag("redis.command_type", command.Command.ToString()); // redis sorgu tipi
                        activity.SetTag("redis.ElapsedTime" ,command.ElapsedTime); // redis sorgusunun ne kadar sürdüğü bilgisi 
                    };

                    redisOptions.EnrichActivityWithTimingEvents = true; // redis sorgularının ne kadar sürdüğünü trace içerisinde görmek için
                   
                }); // redis ile yapılan isteklerin trace içerisinde görmek için

                options.AddConsoleExporter(); // console'a trace bilgilerini yazdırmak için
                options.AddOtlpExporter(); // otel-collector ile trace bilgilerini göndermek için
            }
);

        }
    }
}
