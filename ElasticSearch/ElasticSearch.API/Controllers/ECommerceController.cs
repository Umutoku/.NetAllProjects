using ElasticSearch.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearch.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ECommerceController : ControllerBase
    {
        private readonly ECommerceRepository _eCommerceRepository;

        public ECommerceController(ECommerceRepository eCommerceRepository)
        {
            _eCommerceRepository = eCommerceRepository;
        }

        [HttpGet]
        public async Task<IActionResult> TermQuery(string customerFirstName)
        {
            var result = await _eCommerceRepository.TermQuery(customerFirstName);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> TermsQuery(List<string> customerFirstNameList)
        {
            var result = await _eCommerceRepository.TermsQuery(customerFirstNameList);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> PrefixQuery(string customerFullName)
        {
            var result = await _eCommerceRepository.PrefixQuery(customerFullName);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> RangeQuery(double fromPrice, double toPrice)
        {
            var result = await _eCommerceRepository.RangeQueryAsync(fromPrice, toPrice);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> MatchAll()
        {
            var result = await _eCommerceRepository.MatchAllQueryAsync();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> PaginationQuery(int page=1, int pageSize=10)
        {
            return Ok(await _eCommerceRepository.PaginationQueryAsync(page, pageSize));
        }

        [HttpGet]
        public async Task<IActionResult> WildCardQuery(string customerFullName)
        {
            return Ok(await _eCommerceRepository.WildCardQuery(customerFullName));
        }

        [HttpGet]
        public async Task<IActionResult> FuzzyQuery(string customerFullName)
        {
            return Ok(await _eCommerceRepository.FuzzyQueryAsync(customerFullName));
        }

        [HttpGet]
        public async Task<IActionResult> MatchFullTextQuery(string categoryName)
        {
            return Ok(await _eCommerceRepository.MatchQueryFullTextAsync(categoryName));
        }

        [HttpGet]
        public async Task<IActionResult> MatchBoolPrefixQuery(string categoryName)
        {
            return Ok(await _eCommerceRepository.MatchBooleanPrefixQueryAsync(categoryName));
        }
        [HttpGet]
        public async Task<IActionResult> MatchPhraseQuery(string categoryName)
        {
            return Ok(await _eCommerceRepository.MatchPhraseQueryAsync(categoryName));
        }

        [HttpGet]
        public async Task<IActionResult> CompoundQueryOne(string categoryName, double taxfulTotalPrice)
        {
            return Ok(await _eCommerceRepository.CompoundQueryOneAsync(categoryName, taxfulTotalPrice));
        }


        [HttpGet]
        public async Task<IActionResult> CompoundQueryTwo(string customerFullName)
        {
            return Ok(await _eCommerceRepository.CompoundQueryTwoAsync(customerFullName));
        }

        [HttpGet]
        public async Task<IActionResult> MultiMatchQueryFullText(string name)
        {
            return Ok(await _eCommerceRepository.MultiMatchQueryFullTextAsync(name));
        }
    }
}

