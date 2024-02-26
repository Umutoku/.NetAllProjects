using WebApp.Decorator.Models;

namespace WebApp.Decorator.Repositories.Decorator
{
    public class ProductRepositoryLoggingDecorator : BaseProductRepositoryDecorator
    {
        private readonly ILogger<ProductRepositoryLoggingDecorator> _logger;
        public ProductRepositoryLoggingDecorator(IProductRepository productRepository, ILogger<ProductRepositoryLoggingDecorator> logger) : base(productRepository)
        {
            _logger = logger;
        }

        public async override Task<List<Product>> GetAll(string userId)
        {
            _logger.LogInformation("GetAll method is called");
            return await base.GetAll(userId);
        }

        public async override Task<List<Product>> GetAll()
        {
            _logger.LogInformation("GetAll method is called");
            return await base.GetAll();
        }

        public async override Task<Product> GetById(int id)
        {
            _logger.LogInformation("GetById method is called");
            return await base.GetById(id);
        }

        public async override Task Remove(Product product)
        {
            _logger.LogInformation("Remove method is called");
            await base.Remove(product);
        }

        public async override Task<Product> Save(Product product)
        {
            _logger.LogInformation("Save method is called");
            return await base.Save(product);
        }

        public async override Task Update(Product product)
        {
            _logger.LogInformation("Update method is called");
            await base.Update(product);
        }

    }
}
