using ServiceA.API.Models;

namespace ServiceA.API
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProductService> _logger;

        public ProductService(HttpClient httpClient, ILogger<ProductService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<Product> GetProductById(int id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5001/api/products/{id}");

            if (response.IsSuccessStatusCode)
            {
                var product = await response.Content.ReadFromJsonAsync<Product>();

                _logger.LogInformation("Product {@Product} is retrieved", product);

                return product;
            }

            return null;
        }
    }
}
