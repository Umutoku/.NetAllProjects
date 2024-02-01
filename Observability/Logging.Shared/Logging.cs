using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;

namespace Logging.Shared
{
    public static class Logging
    {
        public static void AddOpenTelemetryLog(this WebApplicationBuilder builder)
        {
            builder.Logging.AddOpenTelemetry(cfg => 
            {
                var serviceName = builder.Configuration["OpenTelemetry:ServiceName"];
                var serviceVersion = builder.Configuration["OpenTelemetry:ServiceVersion"];

                cfg.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName!, serviceVersion));

                cfg.AddOtlpExporter();
            });
        }

        public static Action<HostBuilderContext,LoggerConfiguration> ConfigureLogging =>
            (context, configuration) =>
            {
                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .Enrich.FromLogContext() // Sayesinde loglara request id, user id gibi bilgileri ekler
                    .Enrich.WithExceptionDetails() // Sayesinde exception detaylarını loglara ekler
                    .Enrich.WithProperty("ApplicationName", context.HostingEnvironment.ApplicationName) // Sayesinde loglara uygulama adını ekler
                    .WriteTo.Console();

                var elasticSearchUrl = context.Configuration["ElasticSearch:Url"];
                var userName = context.Configuration["ElasticSearch:UserName"];
                var password = context.Configuration["ElasticSearch:Password"];
                var indexName = context.Configuration["ElasticSearch:IndexName"];

                if (!string.IsNullOrEmpty(elasticSearchUrl))
                {
                    configuration.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticSearchUrl))
                    {
                        AutoRegisterTemplate = true, // Sayesinde logstash-{0:yyyy.MM.dd} formatında index oluşturur
                        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv8, // Elasticsearch 8.0.0 sürümü için index oluşturur
                        IndexFormat = $"{indexName}-{context.HostingEnvironment.EnvironmentName}-logs-"+"{0:yyyy.MM.dd}", // Sayesinde logstash-{0:yyyy.MM.dd} formatında index oluşturur
                        ModifyConnectionSettings = x => x.BasicAuthentication(userName, password), // Sayesinde elastic search'e bağlanırken kullanıcı adı ve şifre ile bağlanır
                        CustomFormatter = new ElasticsearchJsonFormatter()
                    });
                }
            };
    }
}
