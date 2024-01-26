using Elasticsearch.Web.Models;
using Elasticsearch.Web.Repositories;
using Elasticsearch.Web.ViewModel;

namespace Elasticsearch.Web.Services
{
    public class BlogService
    {
        private readonly BlogRepository _blogRepository;
        public BlogService(BlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }
        public async Task<bool> SaveAsync(BlogCreateViewModel blogCreateViewModel)
        {

            var blog = new Blog
            {
                Title = blogCreateViewModel.Title,
                Content = blogCreateViewModel.Content,
                Tags = blogCreateViewModel.Tags.Split(","),
                UserId = Guid.NewGuid()
            };
            return await _blogRepository.SaveAsync(blog) != null;
        }

        public async Task<List<BlogViewModel>> SearchAsync(string searchText)
        {
            var blogList =  await _blogRepository.SearchAsync(searchText);

            return blogList.Select(b=> new BlogViewModel()
            {
                Content = b.Content,
                Created = b.Created.ToShortDateString(),
                Id = b.Id,
                Tags = string.Join(",",b.Tags),
                Title = b.Title,
                UserId = b.UserId.ToString()
            }).ToList();
        }
    }
}
