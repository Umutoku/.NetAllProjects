using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

namespace ElasticSearch.Web.Extensions
{
    public static class Elasticsearch
    {
        public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {
            // Elasticsearch 8

            var settings = new ElasticsearchClientSettings(new Uri(configuration.GetSection("Elastic")["Url"]!));

            var userName = configuration.GetSection("Elastic")["Username"];
            var password = configuration.GetSection("Elastic")["Password"];

            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                settings.Authentication(new BasicAuthentication( userName, password));
            }

            var client = new ElasticsearchClient(settings);

            //Nest 7.0.0

            //SingleNodeConnectionPool pool = new(new Uri(configuration.GetSection("Elastic")["Url"]!));

            //var settings = new ConnectionSettings(pool);

            //settings.DefaultIndex("products");
            //settings.BasicAuthentication(configuration.GetSection("Elastic")["Username"], configuration.GetSection("Elastic")["Password"]);

            //var client = new ElasticClient(settings);

            services.AddSingleton<ElasticsearchClient>(client);

        }
    }
}
