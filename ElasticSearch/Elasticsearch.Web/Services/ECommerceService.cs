using Elasticsearch.Web.Models;
using Elasticsearch.Web.Repositories;
using Elasticsearch.Web.ViewModel;

namespace Elasticsearch.Web.Services
{
    public class ECommerceService
    {
        private readonly ECommerceRepository _eCommerceRepository;

        public ECommerceService(ECommerceRepository eCommerceRepository)
        {
            _eCommerceRepository = eCommerceRepository;
        }

        public async Task<(List<ECommerceViewModel>, long totalCount, long pageLinkCount)> SearchAsync(ECommerceSearchViewModel eCommerceSearchViewModel, int page, int pageSize)
        {
            var (list,totalCount) = await _eCommerceRepository.SearchAsync(eCommerceSearchViewModel, page, pageSize);

            var pageLinkCount = totalCount % pageSize == 0 ? totalCount / pageSize : (totalCount / pageSize) + 1;

            var eCommerceList = list.Select(x => new ECommerceViewModel()
            {
                Category = String.Join(",", x.Category),
                CustomerFirstName = x.CustomerFirstName,
                CustomerFullName = x.CustomerFullName,
                CustomerLastName = x.CustomerLastName,
                OrderDate = x.OrderDate.ToShortDateString(),
                Gender = x.Gender,
                Id = x.Id,
                OrderId = x.OrderId,
                TaxfulTotalPrice = x.TaxfulTotalPrice
            }).ToList();

            return (eCommerceList, totalCount, pageLinkCount);
        }
    }
}
