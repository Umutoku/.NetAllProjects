using ElasticSearch.API.Models;
using Nest;

namespace ElasticSearch.API.Repositories
{
    public class ProductRepository
    {
        private readonly IElasticClient _elasticClient;

        public ProductRepository(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<Product> SaveAsync(Product product)
        {
            product.Created = DateTime.UtcNow;

            var response = await _elasticClient.IndexAsync(product, x => x.Index("products"));

            if (!response.IsValid)
                throw new Exception(response.DebugInformation);

            product.Id = response.Id;

            return product;
        }

        public async Task<Product> GetAsync(string id)
        {
            var response = await _elasticClient.GetAsync<Product>(id);

            if (!response.IsValid)
                throw new Exception(response.DebugInformation);

            return response.Source;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var response = await _elasticClient.SearchAsync<Product>(s => s
                           .MatchAll()
                                          .Size(1000));

            if (!response.IsValid)
                throw new Exception(response.DebugInformation);

            return response.Documents;
        }

        public async Task<IEnumerable<Product>> SearchAsync(string query)
        {
            var response = await _elasticClient.SearchAsync<Product>(s => s
                           .Query(q => q
                           .Match(m => m
                           .Field(f => f.Name)
                           .Query(query))));

            if (!response.IsValid)
                throw new Exception(response.DebugInformation);

            return response.Documents;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            var response = await _elasticClient.IndexDocumentAsync(product);

            if (!response.IsValid)
                throw new Exception(response.DebugInformation);

            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            var response = await _elasticClient.UpdateAsync<Product>(product.Id, u => u
                           .Doc(product)
                                          .RetryOnConflict(5));

            if (!response.IsValid)
                throw new Exception(response.DebugInformation);

            return product;
        }

        public async Task DeleteAsync(string id)
        {
            var response = await _elasticClient.DeleteAsync<Product>(id);

            if (!response.IsValid)
                throw new Exception(response.DebugInformation);
        }
    }
}
