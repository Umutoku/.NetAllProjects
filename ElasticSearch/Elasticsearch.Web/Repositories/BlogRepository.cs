using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elasticsearch.Web.Models;

namespace Elasticsearch.Web.Repositories
{
    public class BlogRepository
    {
        private readonly ElasticsearchClient _client;
        private const string IndexName = "blogs";
        public BlogRepository(ElasticsearchClient client)
        {
            _client = client;
        }
        public async Task<Blog?> SaveAsync(Blog blog)
        {
            blog.Created = DateTime.Now;
            var response = await _client.IndexAsync(blog, IndexName);

            if(!response.IsValidResponse)
            {
                return null;
            }
            return blog;
        }

        public async Task<List<Blog>> SearchAsync(string searchText)
        {
            List<Action<QueryDescriptor<Blog>>> ListQuery = [];

            Action<QueryDescriptor<Blog>> matchAll = q => q.MatchAll();

            Action<QueryDescriptor<Blog>> matchContent = q => q.Match(mt => mt
                      .Field(f => f
                      .Content)
                      .Query(searchText));

            Action<QueryDescriptor<Blog>> matchTitle = q => q.MatchBoolPrefix(mt => mt
            					  .Field(f => f
                                  .Title)
                                  .Query(searchText));

            if(!string.IsNullOrEmpty(searchText))
            {
				ListQuery.Add(matchContent);
				ListQuery.Add(matchTitle);
			}
			else
            {
				ListQuery.Add(matchAll);
			}

			var result = await _client.SearchAsync<Blog>(s => s.Index(IndexName).Size(1000).Query(q => q
           .Bool(b => b
                    .Should(ListQuery.ToArray()
                     //   s => s // should virgül ile ayrılmış birden fazla arama yapılmasını sağlar
                     //.Match(mt => mt.Field(f => f
                     // .Content)
                     // .Query(searchText)), //. diyerek başka arama yaparsak should içinde and gibi çalışır
                     //s=>s.MatchBoolPrefix(p => p
                     // .Field(f => f
                     // .Title)
                     // .Query(searchText))
                     ))));

            foreach (var item in result.Hits)
            {
                item.Source!.Id = item.Id;
            }

            return result.Documents.ToList();

        }

    }
}
