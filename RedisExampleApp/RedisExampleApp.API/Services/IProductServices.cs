using RedisExampleApp.API.Models;

namespace RedisExampleApp.API.Services
{
    public interface IProductServices
    {
        Task<List<Product>> GetAsync();
        Task<Product> GetByIdAsync(int id);
        Task<Product> CreateAsync(Product product);
    }
}
