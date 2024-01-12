using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Moq;
using NuGet.ContentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UdemyRealWorldUnitTest.Web.Controllers;
using UdemyRealWorldUnitTest.Web.Models;
using UdemyRealWorldUnitTest.Web.Repository;

namespace UdemyRealWorldUnitTest.Test
{
    public class ProductsControllerTest
    {
        private readonly Mock<IRepository<Product>> _mockRepo;

        private readonly ProductsController _controller;
        private List<Product> _products;

        public ProductsControllerTest()
        {
            _mockRepo = new Mock<IRepository<Product>>();
            _controller = new ProductsController(_mockRepo.Object);
            _products = new List<Product>()
            {
                new Product(){Id=1,Name="Kalem",Color="Kırmızı",Price=100,Stock=50},
                new Product(){Id=2,Name="Defter",Color="Mavi",Price=200,Stock=500},
            };

        }
        [Fact]
        public async Task GetAll_ActionExecutes_ReturnView()
        {
            var result = await _controller.Index();
            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public async Task GetAll_ActionExecutes_ReturnProductList()
        {
            _mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(_products);
            var result = await _controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var productList = Assert.IsAssignableFrom<IEnumerable<Product>>(viewResult.Model);
            Assert.Equal<int>(2, productList.Count());
        }

        [Fact]
        public async Task Details_IdIsNull_ReturnRedirectToIndexAction()
        {
            var result = await _controller.Details(null);
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public async Task Details_IdInValid_ReturnNotFound()
        {
            Product product = null;
            _mockRepo.Setup(x => x.GetById(0)).ReturnsAsync(product);
            var result = await _controller.Details(0);
            var redirect = Assert.IsType<NotFoundResult>(result);
            Assert.Equal<int>(404, redirect.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public async Task Details_ValidId_ReturnProduct(int id)
        {
            Product product = _products.First(x => x.Id == id);
            _mockRepo.Setup(x => x.GetById(id)).ReturnsAsync(product);
            var result = await _controller.Details(id);
            var viewResult = Assert.IsType<ViewResult>(result);
            var resultProduct = Assert.IsAssignableFrom<Product>(viewResult.Model);
            Assert.Equal<Product>(product, resultProduct);
        }

        [Fact]
        public void Create_ActionExecutes_ReturnView()
        {
            var result = _controller.Create();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task CreatePOST_InValidModelState_ReturnView()
        {
            _controller.ModelState.AddModelError("Name", "Name alanı gereklidir");
            var result = await _controller.Create(_products.First());
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<Product>(viewResult.Model);
        }

        [Fact]
        public async Task CreatePOST_ValidModelState_ReturnRedirectToIndexAction()
        {
            var result = await _controller.Create(_products.First());
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public async Task CreatePOST_ValidModelState_CreateMethodExecute()
        {
            Product product = null;
            _mockRepo.Setup(x => x.Create(It.IsAny<Product>())).Callback<Product>(x => product = x);
            var result = await _controller.Create(_products.First());
            _mockRepo.Verify(x => x.Create(It.IsAny<Product>()), Times.Once);
            Assert.Equal(_products.First().Name, product.Name);
        }

        [Fact]
        public async Task CreatePOST_InValidModelState_NeverCreateExecute()
        {
            _controller.ModelState.AddModelError("Name", "Name alanı gereklidir");
            var result = await _controller.Create(_products.First());
            _mockRepo.Verify(x => x.Create(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public async Task CreatePOST_ValidModelState_ReturnNotFound()
        {
            Product product = null;
            _mockRepo.Setup(x => x.Create(It.IsAny<Product>())).Callback<Product>(x => product = x);
            var result = await _controller.Create(_products.First());
            _mockRepo.Verify(x => x.Create(It.IsAny<Product>()), Times.Once);
            Assert.Equal(_products.First().Name, product.Name);
        }

        [Fact]
        public async Task Edit_IdIsNull_ReturnNotFound()
        {
            var result = await _controller.Edit(null);
            Assert.IsType<NotFoundResult>(result);
            Asset.Equals(404, (result as NotFoundResult).StatusCode);
        }

        [Theory]
        [InlineData(0)]
        public async Task Edit_IdInValid_ReturnNotFound(int id)
        {
            Product product = null;
            _mockRepo.Setup(x => x.GetById(id)).ReturnsAsync(product);
            var result = await _controller.Edit(id);
            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1)]
        public async Task Edit_IdValid_ReturnViewResult(int id)
        {
            Product product = _products.First(x => x.Id == id);
            _mockRepo.Setup(x => x.GetById(id)).ReturnsAsync(product);
            var result = await _controller.Edit(id);
            var viewResult = Assert.IsType<ViewResult>(result);
            var resultProduct = Assert.IsAssignableFrom<Product>(viewResult.Model);
            Assert.Equal<Product>(product, resultProduct);
        }

        [Theory]
        [InlineData(1)]
        public async Task Edit_IdValid_ReturnProduct(int id)
        {
            Product product = _products.First(x => x.Id == id);
            _mockRepo.Setup(x => x.GetById(id)).ReturnsAsync(product);
            var result = await _controller.Edit(id);
            var viewResult = Assert.IsType<ViewResult>(result);
            var resultProduct = Assert.IsAssignableFrom<Product>(viewResult.Model);
            Assert.Equal<int>(id, resultProduct.Id);

        }

        [Theory]
        [InlineData(1)]
        public async Task EditPOST_IdIsNotEqualProduct_ReturnNotFound(int id)
        {
            var result = await _controller.Edit(2, _products.First());
            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1)]
        public async Task EditPOST_IdIsValid_ReturnRedirectToIndexAction(int id)
        {
            var result = await _controller.Edit(id, _products.First());
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Theory]
        [InlineData(1)]
        public async Task EditPOST_IdIsValid_UpdateMethodExecute(int id)
        {
            Product result = null;
            _mockRepo.Setup(x => x.Update(It.IsAny<Product>())).Callback<Product>(x => result = x);
            var resultAction = await _controller.Edit(id, _products.First());
            _mockRepo.Verify(x => x.Update(It.IsAny<Product>()), Times.Once);
            Assert.Equal(_products.First().Name, result.Name);
        }

        [Theory]
        [InlineData(1)]
        public async Task EditPOST_InValidModelState_ReturnView(int id)
        {
            _controller.ModelState.AddModelError("Name", "Name alanı gereklidir");
            var result = await _controller.Edit(id, _products.First());
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<Product>(viewResult.Model);
        }

        [Fact]
        public async Task Delete_IdIsNull_ReturnNotFound()
        {
            var result = await _controller.Delete(null);
            Assert.IsType<NotFoundResult>(result);
            Asset.Equals(404, (result as NotFoundResult).StatusCode);
        }

        [Theory]
        [InlineData(0)]
        public async Task Delete_IdInValid_ReturnNotFound(int id)
        {
            Product product = null;
            _mockRepo.Setup(x => x.GetById(id)).ReturnsAsync(product);
            var result = await _controller.Delete(id);
            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1)]
        public async Task Delete_IdValid_ReturnViewResult(int id)
        {
            Product product = _products.First(x => x.Id == id);
            _mockRepo.Setup(x => x.GetById(id)).ReturnsAsync(product);
            var result = await _controller.Delete(id);
            var viewResult = Assert.IsType<ViewResult>(result);
            var resultProduct = Assert.IsAssignableFrom<Product>(viewResult.Model);
            Assert.Equal<Product>(product, resultProduct);
        }

        [Theory]
        [InlineData(1)]
        public async Task DeletePOST_IdIsNotEqualProduct_ReturnNotFound(int id)
        {
            var result = await _controller.DeleteConfirmed(2);
            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1)]
        public async Task DeletePOST_IdIsValid_ReturnRedirectToIndexAction(int id)
        {
            var result = await _controller.DeleteConfirmed(id);
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteConfirmed_IdIsValid_DeleteMethodExecute(int id)
        {
            Product result = null;
            _mockRepo.Setup(x => x.Delete(It.IsAny<Product>())).Callback<Product>(x => result = x);
            var resultAction = await _controller.DeleteConfirmed(id);
            _mockRepo.Verify(x => x.Delete(It.IsAny<Product>()), Times.Once);
            Assert.Equal(id, result.Id);
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteConfirmed_ActionExecutes_ReturnView(int id)
        {
            var result = await _controller.DeleteConfirmed(id);
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", viewResult.ActionName);
        }



    }
}
