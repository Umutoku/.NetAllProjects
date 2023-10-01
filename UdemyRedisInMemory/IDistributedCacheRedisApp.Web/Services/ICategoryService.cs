using IDistributedCacheRedisApp.Web.Models;

namespace IDistributedCacheRedisApp.Web.Services
{
    public interface ICategoryService
    {
        List<CategoryModel> GetAllCategory();
    }
}
