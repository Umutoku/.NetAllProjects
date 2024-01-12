using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyRealWorldUnitTest.Web.Controllers;
using UdemyRealWorldUnitTest.Web.Models;

namespace UdemyRealWorldUnitTest.Test
{
    public class ProductsCategoriesControllerTestInMemory : ProductsCategoriesControllerTest
    {
        public ProductsCategoriesControllerTestInMemory()
        {
            SetContextOptions(ContextOptions);
        }

        public DbContextOptions<UdemyUnitTestDBContext> ContextOptions
        {
            get
            {
                var options = new DbContextOptionsBuilder<UdemyUnitTestDBContext>()
                    .UseInMemoryDatabase(databaseName: "TestDB")
                    .Options;
                return options;
            }
        }
        [Fact]
        public async Task Create_ModelValidProduct_ReturnsRedirectToActionWithSaveProduct()
        {
            var product = new Product { Name = "P3", Price = 30, Stock = 30, Color = "Green" };

            using (UdemyUnitTestDBContext context = new UdemyUnitTestDBContext(ContextOptions))
            {
                var category = await context.Category.FirstOrDefaultAsync();

                product.CategoryId = category.Id;

                var controller = new ProductsCategoriesController(context);

                var result = await controller.Create(product);
                Assert.NotNull(result);

                var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

                Assert.Equal("Index", redirectToActionResult.ActionName);

            }

            using (UdemyUnitTestDBContext context = new UdemyUnitTestDBContext(ContextOptions))
            {
                var productNew = await context.Product.FirstOrDefaultAsync(x=>x.Name==product.Name);
                Assert.Equal(product.Name, productNew.Name);
            }
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteCategory_ExistCategoryId_DeletedAllProducts(int categoryId)
        {
            using (UdemyUnitTestDBContext context = new UdemyUnitTestDBContext(ContextOptions))
            {
                var category = await context.Category.FirstOrDefaultAsync(x => x.Id == categoryId);

                context.Remove(category);

                await context.SaveChangesAsync();
            }

            using (UdemyUnitTestDBContext context = new UdemyUnitTestDBContext(ContextOptions))
            {
                var products = await context.Product.Where(x => x.CategoryId == categoryId).ToListAsync();
                Assert.Empty(products);
            }
        }
    }
}
