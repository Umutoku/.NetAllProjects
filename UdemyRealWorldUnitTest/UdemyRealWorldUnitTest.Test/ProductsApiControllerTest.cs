using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyRealWorldUnitTest.Web.Controllers;
using UdemyRealWorldUnitTest.Web.Helper;
using UdemyRealWorldUnitTest.Web.Models;
using UdemyRealWorldUnitTest.Web.Repository;

namespace UdemyRealWorldUnitTest.Test
{
    public class ProductsApiControllerTest
    {
        private readonly Mock<IRepository<Product>> _mockRepo;
        private readonly ProductsApiController _controller;
        private List<Product> products;
        private readonly Helper helper;

        public ProductsApiControllerTest()
        {
            _mockRepo = new Mock<IRepository<Product>>();
            _controller = new ProductsApiController(_mockRepo.Object);
            products = new List<Product>()
            {
                new Product(){Id=1,Name="Kalem",Price=100,Stock=50,Color="Kırmızı"},
                new Product(){Id=2,Name="Defter",Price=200,Stock=500,Color="Mavi"}
            };
            helper = new Helper();
        }

        [Theory]
        [InlineData(3, 5)]
        [InlineData(10, 20)]
        public void Add_SampleValues_ReturnSum(int a, int b)
        {
            var result = helper.AddTwoInt(a, b);

            Assert.Equal(a + b, result);
        }

        [Fact]
        public async void GetProduct_ActionExecutes_ReturnOkResultWithProduct()
        {
            _mockRepo.Setup(x => x.GetAll()).ReturnsAsync(products);

            var result = await _controller.GetProduct();

            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnProducts = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);

            Assert.Equal<int>(2, returnProducts.ToList().Count);
        }

        [Theory]
        [InlineData(0)]
        public async void GetProduct_IdInValid_ReturnNotFound(int id)
        {
            Product product = null;
            _mockRepo.Setup(x => x.GetById(id)).ReturnsAsync(product);

            var result = await _controller.GetProduct(id);

            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void GetProduct_IdValid_ReturnOkResult(int id)
        {
            var product = products.First(x => x.Id == id);
            _mockRepo.Setup(x => x.GetById(id)).ReturnsAsync(product);

            var result = await _controller.GetProduct(id);

            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnProduct = Assert.IsType<Product>(okResult.Value);

            Assert.Equal(id, returnProduct.Id);
            Assert.Equal(product.Name, returnProduct.Name);
            
        }

        [Theory]
        [InlineData(1)]
        public async void PutProduct_IdIsNotEqualProduct_ReturnBadRequestResult(int id)
        {
            var product = products.First(x => x.Id == id);

            var result = await _controller.PutProduct(2, product);

            var badRequestStatus = Assert.IsType<BadRequestResult>(result);

            Assert.Equal(400, badRequestStatus.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public async void PutProduct_ActionExecutes_ReturnNoContent(int id)
        {
            var product = products.First(x => x.Id == id);

            _mockRepo.Setup(x => x.Update(product));

            var result = await _controller.PutProduct(id, product);

            _mockRepo.Verify(x => x.Update(product), Times.Once);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void PostProduct_ActionExecutes_ReturnCreatedAtAction()
        {
            var product = products.First();

            _mockRepo.Setup(x => x.Create(product)).Returns((Task<Product>)Task.CompletedTask);

            var result = await _controller.PostProduct(product);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);

            _mockRepo.Verify(x => x.Create(product), Times.Once);

            Assert.Equal("GetProduct", createdAtActionResult.ActionName);
        }

        [Fact]
        public async void DeleteProduct_ActionExecutes_ReturnNoContent()
        {
            var product = products.First();

            _mockRepo.Setup(x => x.GetById(product.Id)).ReturnsAsync(product);

            _mockRepo.Setup(x => x.Delete(product));

            var result = await _controller.DeleteProduct(product.Id);

            _mockRepo.Verify(x => x.GetById(product.Id), Times.Once);

            _mockRepo.Verify(x => x.Delete(product), Times.Once);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void DeleteProduct_IdInValid_ReturnNotFound()
        {
            Product product = null;

            _mockRepo.Setup(x => x.GetById(0)).ReturnsAsync(product);

            var result = await _controller.DeleteProduct(0);

            _mockRepo.Verify(x => x.GetById(0), Times.Once);

            Assert.IsType<NotFoundResult>(result);
        }

        

    }
}
