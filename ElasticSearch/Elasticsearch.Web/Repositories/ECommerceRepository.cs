using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elasticsearch.Web.Models;
using Elasticsearch.Web.ViewModel;

namespace Elasticsearch.Web.Repositories
{
    public class ECommerceRepository
    {
        private readonly ElasticsearchClient _elasticClient;

        private const string IndexName = "ecommerce";

        public ECommerceRepository(ElasticsearchClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<(List<ECommerce>,long count)> SearchAsync(ECommerceSearchViewModel eCommerceSearchViewModel, int page, int pageSize)
        {
            List<Action<QueryDescriptor<ECommerce>>> listQuery = [];

            if (eCommerceSearchViewModel is null)
            {
                listQuery.Add(q => q.MatchAll());
                return await CalculateResultSet(page, pageSize, listQuery);
            }

            if (!string.IsNullOrEmpty(eCommerceSearchViewModel.Category))
            {
                listQuery.Add(q => q.Match(mt => mt
                                   .Field(f => f.Category)
                                                      .Query(eCommerceSearchViewModel.Category)));
            }

            if (!string.IsNullOrEmpty(eCommerceSearchViewModel.CustomerFullName))
            {
                listQuery.Add(q => q.Match(mt => mt
                                   .Field(f => f.CustomerFullName)
                                                       .Query(eCommerceSearchViewModel.CustomerFullName)));
            }

            if (eCommerceSearchViewModel.OrderDateStart.HasValue)
            {
                listQuery.Add(q => q.Range(r => r.DateRange(mt => mt
                                                  .Field(f => f.OrderDate)
                                                  .Gte(eCommerceSearchViewModel.OrderDateStart.Value))));
            }

            if (eCommerceSearchViewModel.OrderDateEnd.HasValue)
            {
                listQuery.Add(q => q.Range(r => r.DateRange(mt => mt
                                                   .Field(f => f.OrderDate)
                                                   .Lte(eCommerceSearchViewModel.OrderDateEnd.Value))));
            }

            if (!string.IsNullOrEmpty(eCommerceSearchViewModel.Gender))
            {
                listQuery.Add(q => q.Term(t => t.Field(f => f.Gender).Value(eCommerceSearchViewModel.Gender).CaseInsensitive()));
            }

            if (!listQuery.Any())
            {
                listQuery.Add(q => q.MatchAll());
            }

            return await CalculateResultSet(page, pageSize, listQuery);

        }

        private async Task<(List<ECommerce> list, long count)> CalculateResultSet(int page, int pageSize, List<Action<QueryDescriptor<ECommerce>>> listQuery)
        {
            var pageFrom = (page - 1) * pageSize;

            var result = await _elasticClient.SearchAsync<ECommerce>(s => s.Index(IndexName).Size(pageSize).From(pageFrom).Query(q => q.Bool(b => b.Must(listQuery.ToArray()))));

            foreach (var item in result.Hits)
            {
                item.Source!.Id = item.Id;
            }

            return (result.Documents.ToList(), result.Total);
        }
    }
}
