using Elastic.Clients.Elasticsearch;
using ElasticSearch.API.DTOs;
using ElasticSearch.API.Models;
//using Nest;
using System.Collections.Immutable;

namespace ElasticSearch.API.Repositories
{
    public class ProductRepository
    {
        //private readonly IElasticClient _elasticClient;
        private readonly ElasticsearchClient _elasticsearchClient;

        private const string indexName = "products";

        public ProductRepository(/*IElasticClient elasticClient*/ ElasticsearchClient elasticsearchClient)
        {
            _elasticsearchClient = elasticsearchClient;
        }

        public async Task<Product> SaveAsync(Product product)
        {
            product.Created = DateTime.UtcNow;

            var response = await _elasticsearchClient.IndexAsync(product, x => x.Index("products").Id(Guid.NewGuid()));

            if (!response.IsValidResponse)
                throw new Exception(response.DebugInformation);

            product.Id = response.Id;

            return product;
        }

        public async Task<Product> GetAsync(string id)
        {
            var response = await _elasticsearchClient.GetAsync<Product>(id,x=>x.Index(indexName));

            if (!response.IsValidResponse)
                throw new Exception(response.DebugInformation);

            response.Source!.Id = response.Id;
            return response.Source;
        }

        public async Task<ImmutableList<Product>> GetAllAsync()
        {
            var response = await _elasticsearchClient.SearchAsync<Product>(s => s
                           .Index(indexName)
                           .Query(q => q.MatchAll()));
                           

            if (!response.IsValidResponse)
                throw new Exception(response.DebugInformation);

            foreach (var item in response.Hits)
            {
                item.Source!.Id = item.Id;
            }

            return response.Documents.ToImmutableList();
        }

        public async Task<bool> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            //var response = await _elasticsearchClient.UpdateAsync<Product, ProductUpdateDto>(productUpdateDto.Id, xs => xs.Index(indexName).Doc(productUpdateDto));

            var response = await _elasticsearchClient.UpdateAsync<Product,ProductUpdateDto>(indexName,productUpdateDto.Id, x => x.Doc(productUpdateDto));

            return response.IsValidResponse;
        }
        /// <summary>
        /// Hata yönetimi için bu method ele alındı.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DeleteResponse> DeleteAsync(string id)
        {
            var response = await _elasticsearchClient.DeleteAsync<Product>(id, x => x.Index(indexName));

            return response;
        }
    }
}
