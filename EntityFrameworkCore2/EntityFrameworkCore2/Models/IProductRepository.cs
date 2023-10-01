using System.Linq;

namespace EntityFrameworkCore2.Models
{
    public interface IProductRepository
    {
         IQueryable<Product> Product { get; }
    }
}
