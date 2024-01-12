using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyRealWorldUnitTest.Web.Models;

namespace UdemyRealWorldUnitTest.Test
{
    public class ProductsCategoriesControllerTest
    {
        protected DbContextOptions<UdemyUnitTestDBContext> _contextOptions { get; private set; }

        public void SetContextOptions(DbContextOptions<UdemyUnitTestDBContext> contextOptions)
        {
            _contextOptions = contextOptions;
            SeedAsync();
        }

        public async Task SeedAsync()
        {
            using (UdemyUnitTestDBContext context = new UdemyUnitTestDBContext(_contextOptions))
            {
                context.Database.EnsureDeleted(); // Delete the database.
                context.Database.EnsureCreated(); // Create the database.

                await context.Product.AddRangeAsync(
                    new Product { Id = 1, Name = "P1", Price = 10, Stock = 10, Color = "Red", CategoryId = 1 },
                    new Product { Id = 2, Name = "P2", Price = 20, Stock = 20, Color = "Blue", CategoryId = 1 }
                    );
                await context.SaveChangesAsync();

                await context.Category.AddRangeAsync(
                    new Category { Name = "C1" },
                    new Category { Name = "C2" }
                    );
                await context.SaveChangesAsync();
            }
        }
    }
}
