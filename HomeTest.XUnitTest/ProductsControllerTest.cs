using HomeTest.Controllers;
using HomeTest.Data.Entities;
using HomeTest.Services.Core;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HomeTest.XUnitTest
{
    public class ProductsControllerTest
    {
        [Fact]
        public async Task Index_ReturnsViewResultWithModel()
        {
            var productLists = new List<Product>();
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Keyboard",
                Description = "",
                Quantity = 10,
                Enable = true
            };
            productLists.Add(product);

            var mockService = new Mock<IEntityService<Product>>();
            mockService.Setup(s => s.FindByConditionAsync(p => p.Enable)).Returns(Task.FromResult(productLists.AsEnumerable()));
            var controller = new ProductsController(mockService.Object);

            // Act
            var result = await controller.Index(); // as ViewResult;

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Product>>(viewResult.ViewData.Model);
            Assert.NotNull(viewResult);
            Assert.NotEmpty(model);
            Assert.Single(model);
            Assert.NotNull(viewResult.Model);
            Assert.Equal(typeof(List<Product>), viewResult.Model.GetType());
        }

        [Theory(DisplayName = "Detail product item")]
        [InlineData("Keyboard")]
        public async Task Details_ReturnsViewResultWithModel(string expectedName)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Keyboard",
                Description = "",
                Quantity = 10,
                Enable = true
            };

            var mockService = new Mock<IEntityService<Product>>();
            mockService.Setup(s => s.GetByIdAsync(product.Id)).Returns(Task.FromResult(product));
            var controller = new ProductsController(mockService.Object);

            // Act
            var result = await controller.Details(product.Id); // as ViewResult;

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Product>(viewResult.ViewData.Model);
            Assert.NotNull(viewResult);
            Assert.NotNull(model);
            Assert.NotNull(viewResult.Model);
            Assert.Equal(expectedName, model.Name);
            Assert.Equal(10, model.Quantity);
            Assert.Equal(typeof(Product), viewResult.Model.GetType());
        }

        [Fact]
        public async Task Create_ReturnsViewResultWithModel()
        {
            var product = new Product
            {
                Name = "Keyboard",
                Description = "",
                Quantity = 10,
            };

            var mockService = new Mock<IEntityService<Product>>();
            var controller = new ProductsController(mockService.Object);

            var result = await controller.Create(product); // as ViewResult;

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.NotNull(viewResult);
            Assert.Equal(nameof(controller.Index), viewResult.ActionName);
        }

        [Theory(DisplayName = "Edit product item")]
        [InlineData("Keyboard")]
        public async Task Edit_ReturnsViewResultWithModel(string expectedName)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Keyboard",
                Description = "",
                Quantity = 10,
                Enable = true
            };

            var mockService = new Mock<IEntityService<Product>>();
            mockService.Setup(s => s.GetByIdAsync(product.Id)).Returns(Task.FromResult(product));
            var controller = new ProductsController(mockService.Object);

            // Act
            var result = await controller.Edit(product.Id); // as ViewResult;

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Product>(viewResult.ViewData.Model);
            Assert.NotNull(viewResult);
            Assert.NotNull(model);
            Assert.NotNull(viewResult.Model);
            Assert.Equal(expectedName, model.Name);
            Assert.Equal(10, model.Quantity);
            Assert.Equal(typeof(Product), viewResult.Model.GetType());
        }

        [Fact]
        public async Task Delete_ReturnsNotFoundResult()
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Keyboard",
                Description = "",
                Quantity = 10,
                Enable = true
            };

            var mockService = new Mock<IEntityService<Product>>();
            var controller = new ProductsController(mockService.Object);

            var result = await controller.DeleteConfirmed(product.Id); // as ViewResult;

            var viewResult = Assert.IsType<NotFoundResult>(result);
            Assert.NotNull(viewResult);
            Assert.Equal(404, viewResult.StatusCode);
        }
    }
}