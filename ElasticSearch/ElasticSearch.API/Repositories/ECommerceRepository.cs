using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ElasticSearch.API.Models;
using System.Collections.Immutable;
using System.Security.Cryptography;

namespace ElasticSearch.API.Repositories
{
    public class ECommerceRepository
    {
        private readonly ElasticsearchClient _elasticsearchClient;
        private const string indexName = "kibana_sample_data_ecommerce";

        public ECommerceRepository(ElasticsearchClient elasticsearchClient)
        {
            _elasticsearchClient = elasticsearchClient;
        }
        // Term Query sayesinde tek bir değer ile arama yapabiliriz.
        public async Task<ImmutableList<ECommerce>> TermQuery(string customerFirstName)
        {
            //1. way

            //var response = await _elasticsearchClient.SearchAsync<ECommerce>(s => s.Index(indexName).Query(q => q.Term(t => t.Field("customer_first_name.keyword").Value(customerFirstName))));

            //2. way

            //var response = await _elasticsearchClient.SearchAsync<ECommerce>(s => s.Index(indexName).Query(q => q.Term(t => t.CustomerFirstName.Suffix("keyword"),customerFirstName)));

            //3. way

            var termQuery = new TermQuery("customer_first_name.keyword")
            {
                Value = customerFirstName,
                CaseInsensitive = true
            };

            var response = await _elasticsearchClient.SearchAsync<ECommerce>(s => s.Index(indexName).Query(termQuery));

            if (!response.IsValidResponse)
                throw new Exception(response.DebugInformation);

            foreach (var hit in response.Hits)
            {
                hit.Source!.Id = hit.Id;
            }

            return response.Documents.ToImmutableList();
        }
        // Terms Query sayesinde birden fazla değer ile arama yapabiliriz.
        public async Task<ImmutableList<ECommerce>> TermsQuery(List<string> customerFirstNameList)
        {
            List<FieldValue> terms = [];
            customerFirstNameList.ForEach(x => terms.Add(x));

            // 1.way
            //var termsQuery = new TermsQuery()
            //{
            //    Field = "customer_first_name.keyword",
            //    Terms = new TermsQueryField(terms.AsReadOnly())
            //};

            //var response = await _elasticsearchClient.SearchAsync<ECommerce>(s => s.Index(indexName).Query(termsQuery));

            //2.way
            var response = await _elasticsearchClient.SearchAsync<ECommerce>(s => s.Index(indexName).Size(100)
            .Query(q => q
            .Terms(t => t
            .Field(f => f.CustomerFirstName.Suffix("keyword"))
            .Terms(new TermsQueryField(terms.AsReadOnly())))));

            foreach (var hit in response.Hits)
            {
                hit.Source!.Id = hit.Id;
            }

            return response.Documents.ToImmutableList();
        }
        // Prefix Query sayesinde başlangıç harflerine göre arama yapabiliriz.
        public async Task<ImmutableList<ECommerce>> PrefixQuery(string customerFullName)
        {
            var response = await _elasticsearchClient.SearchAsync<ECommerce>(s => s.Index(indexName)
            .Query(q => q.Prefix(t => t
            .Field(f => f.CustomerFullName.Suffix("keyword"))
            .Value(customerFullName))));

            foreach (var hit in response.Hits)
            {
                hit.Source!.Id = hit.Id;
            }

            return response.Documents.ToImmutableList();
        }
        // Range Query sayesinde aralık belirterek arama yapabiliriz.
        public async Task<ImmutableList<ECommerce>> RangeQueryAsync(double fromPrice, double toPrice)
        {
            var result = await _elasticsearchClient.SearchAsync<ECommerce>(s => s.Index(indexName)
            .Query(q => q
            .Range(r => r
            .NumberRange(nr => nr
            .Field(f => f
            .TaxfulTotalPrice).Gte(fromPrice).Lte(toPrice)))));

            foreach (var hit in result.Hits)
            {
                hit.Source!.Id = hit.Id;
            }

            return result.Documents.ToImmutableList();
        }
        // MatchAll Query sayesinde tüm verileri çekebiliriz.
        public async Task<ImmutableList<ECommerce>> MatchAllQueryAsync()
        {
            var result = await _elasticsearchClient.SearchAsync<ECommerce>(s => s.Index(indexName).Size(100).Query(q => q.MatchAll()));

            foreach (var hit in result.Hits)
            {
                hit.Source!.Id = hit.Id;
            }
            return result.Documents.ToImmutableList();
        }
        // Pagination Query sayesinde sayfalama yapabiliriz.
        public async Task<ImmutableList<ECommerce>> PaginationQueryAsync(int page, int pageSize)
        {
            var pageFrom = (page - 1) * pageSize;

            var result = await _elasticsearchClient.SearchAsync<ECommerce>(s => s.Index(indexName).Size(pageSize).From(pageFrom).Query(q => q.MatchAll()));

            foreach (var hit in result.Hits)
            {
                hit.Source!.Id = hit.Id;
            }
            return result.Documents.ToImmutableList();
        }
        // WildCard Query sayesinde arama yaparken yazım hatası yapmamıza rağmen sonuç döndürür. ? ve * ile arama yapabiliriz.
        public async Task<ImmutableList<ECommerce>> WildCardQuery(string customerFullName)
        {
            var result = await _elasticsearchClient.SearchAsync<ECommerce>(s => s.Index(indexName).Size(100).Query(q => q.Wildcard(w => w.Field(f => f.CustomerFullName.Suffix("keyword")).Value(customerFullName))));

            foreach (var hit in result.Hits)
            {
                hit.Source!.Id = hit.Id;
            }

            return result.Documents.ToImmutableList();
        }

        // Fuzzy Query sayesinde arama yaparken yazım hatası yapmamıza rağmen sonuç döndürür.
        public async Task<ImmutableList<ECommerce>> FuzzyQueryAsync(string customerFullName)
        {
            var result = await _elasticsearchClient.SearchAsync<ECommerce>(s => s.Index(indexName).Size(100).Query(q => q.Fuzzy(f => f.Field(f => f.CustomerFullName.Suffix("keyword")).Value(customerFullName).Fuzziness(new Fuzziness(1)))).Sort(s => s.Field(f => f.TaxfulTotalPrice, new FieldSort() { Order = SortOrder.Desc }))); //SortOrder ile sıralama yapabiliriz.

            foreach (var hit in result.Hits)
            {
                hit.Source!.Id = hit.Id;
            }

            return result.Documents.ToImmutableList();
        }
        // Match Query sayesinde arama yapabiliriz. Fakat bu arama tam eşleşme yapar. Tam eşleşme yapmak istemiyorsak Match Query Full Text kullanmalıyız.
        public async Task<ImmutableList<ECommerce>> MatchQueryFullTextAsync(string categoryName)
        {
            var result = await _elasticsearchClient.SearchAsync<ECommerce>(s => s.Index(indexName).Size(1000).Query(q => q.Match(m => m.Field(f => f.Category).Query(categoryName)
            .Operator(Operator.And) // Operator.And ile arama yaparken tüm kelimelerin geçmesini sağlayabiliriz.
            )));

            foreach (var hit in result.Hits)
            {
                hit.Source!.Id = hit.Id;
            }

            return result.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<ECommerce>> MultiMatchQueryFullTextAsync(string name)
        {
            var result = await _elasticsearchClient.SearchAsync<ECommerce>(s => s.Index(indexName).Size(1000).Query(q => q
            .MultiMatch(mm => mm
                .Fields
                 (new Field("customer_first_name").And
                 (new Field("customer_last_name")).And
                 (new Field("customer_full_name"))).Query(name))));

            foreach (var hit in result.Hits)
            {
                hit.Source!.Id = hit.Id;
            }

            return result.Documents.ToImmutableList();
        }

        // Match Boolean Prefix Query sayesinde arama yapabiliriz. Fakat bu arama tam eşleşme yapar. Tam eşleşme yapmak istemiyorsak Match Query Full Text kullanmalıyız.
        public async Task<ImmutableList<ECommerce>> MatchBooleanPrefixQueryAsync(string categoryName)
        {
            var result = await _elasticsearchClient.SearchAsync<ECommerce>(s => s.Index(indexName).Size(1000).Query(q => q
            .MatchBoolPrefix(m => m // MatchBoolPrefix ile arama yaparken son kelime prefix ile aranır.  
            .Field(f => f.Category).Query(categoryName))));

            foreach (var hit in result.Hits)
            {
                hit.Source!.Id = hit.Id;
            }

            return result.Documents.ToImmutableList();

        }

        // Match Phrase Query sayesinde arama yapabiliriz. 
        public async Task<ImmutableList<ECommerce>> MatchPhraseQueryAsync(string categoryName)
        {
            var result = await _elasticsearchClient.SearchAsync<ECommerce>(s => s.Index(indexName).Size(1000).Query(q => q
            .MatchPhrase(m => m // MatchPhrase ile arama yaparken tüm kelimelerin geçmesini sağlayabiliriz. Öncesi ya da sonrası önemli değil
            .Field(f => f.Category).Query(categoryName))));

            foreach (var hit in result.Hits)
            {
                hit.Source!.Id = hit.Id;
            }

            return result.Documents.ToImmutableList();

        }

        // Compound Query sayesinde birden fazla sorgu ile arama yapabiliriz. 
        public async Task<ImmutableList<ECommerce>> CompoundQueryOneAsync(string categoryName,double taxfulTotalPrice)
        {
            var result = await _elasticsearchClient.SearchAsync<ECommerce>(s => s.Index(indexName).Size(1000).Query(q => q
                       .Bool(b => b
                                .Must(m => m
                                  .Term(t => t.Field("geoip.city_name")
                                  .Value("İstanbul")))
                                .MustNot(mt => mt //
                                  .Range(r => r
                                  .NumberRange(nr => nr
                                  .Field(f => f
                                  .TaxfulTotalPrice)
                                  .Lte(taxfulTotalPrice))))
                                .Should(s=>s
                                  .Term(t=>t
                                  .Field(ff=>ff.Category.Suffix("keyword"))
                                  .Value(categoryName)))
                                .Filter(f=>f.Term(t=>t.Field("manufacturer.keyword").Value("Tigress Enterprises"))))));

            foreach (var hit in result.Hits)
            {
                hit.Source!.Id = hit.Id;
            }

            return result.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<ECommerce>> CompoundQueryTwoAsync(string customerFullName)
        {
            var result = await _elasticsearchClient.SearchAsync<ECommerce>(s => s.Index(indexName).Size(1000).Query(q => q
                       .Bool(b => b
                                .Should(m => m
                                 .Match(mt => mt.Field(f => f
                                  .CustomerFullName)
                                  .Query(customerFullName))
                                 .Prefix(p=>p
                                  .Field(f=>f
                                  .CustomerFullName.Suffix("keyword"))
                                  .Value(customerFullName))))));

            foreach (var hit in result.Hits)
            {
                hit.Source!.Id = hit.Id;
            }

            return result.Documents.ToImmutableList();
        }

        
    }
}
