﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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


    }
}
