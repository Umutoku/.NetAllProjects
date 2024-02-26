using Microsoft.Extensions.Caching.Memory;
using System.Configuration;
using System.Runtime.CompilerServices;
using WebApp.Decorator.Models;

namespace WebApp.Decorator.Repositories.Decorator
{
    public class ProductRepositoryCacheDecorator : BaseProductRepositoryDecorator
    {
        private readonly IMemoryCache _memoryCache;
        private const string CacheKey = "Products";
        public ProductRepositoryCacheDecorator(IProductRepository productRepository, IMemoryCache memoryCache) : base(productRepository)
        {
            _memoryCache = memoryCache;
        }

        public async override Task<List<Product>> GetAll()
        {
            if(_memoryCache.TryGetValue(CacheKey, out List<Product> products)) // önbellekten veri alınır ve products değişkenine atanır
            {
                return products;
            }
            await UpdateCache();

            return _memoryCache.Get(CacheKey) as List<Product>; // önbelleğe alınan Products anahtarı ile önbellekten veri alınır
        }

        public async override Task<List<Product>> GetAll(string userId)
        {
            var products = await base.GetAll(userId);

            return products.Where(x => x.UserId == userId).ToList();
        }

        public async override Task<Product> Save(Product product)
        {
            var result = await base.Save(product);
            await UpdateCache();
            return result;
        }

        public async override Task Update(Product product)
        {
            await base.Update(product);
            await UpdateCache();
        }

        public async override Task Remove(Product product)
        {
            await base.Remove(product);
            await UpdateCache();
        }

        private async Task UpdateCache()
        {            
            _memoryCache.Set(CacheKey, base.GetAll().Result, TimeSpan.FromMinutes(10)); // sayesinde Products anahtarı ile 10 dakika boyunca önbelleğe alınır
         }
    }
}
