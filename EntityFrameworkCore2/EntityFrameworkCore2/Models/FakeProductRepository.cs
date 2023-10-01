using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkCore2.Models
{
    public class FakeProductRepository : IProductRepository
    {
        public IQueryable<Product> Product => new List<Product>
        {
            new Product()
            {ProductId=1,Name="Samsung S5",Price=2000,Category="Telefon"},
            new Product()
            {ProductId=2,Name="Samsung S6",Price=2000,Category="Telefon"},

        }.AsQueryable();
    }
}
