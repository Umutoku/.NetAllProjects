using Elasticsearch.Net;
using Nest;

namespace ElasticSearch.API.Extensions
{
    public static class Elasticsearch
    {
        public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {
            SingleNodeConnectionPool pool = new(new Uri(configuration.GetSection("Elastic")["Url"]!));

            var settings = new ConnectionSettings(pool);

            //settings.DefaultIndex("products");
            //settings.BasicAuthentication(configuration.GetSection("Elastic")["Username"], configuration.GetSection("Elastic")["Password"]);

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);

        }
    }
}
