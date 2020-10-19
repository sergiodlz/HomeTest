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
    public class ProductsClientControllerTest
    {
        [Fact]
        public async Task GetProducts()
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
            var controller = new ProductsClientController(mockService.Object);

            // Act
            var result = await controller.GetProducts();

            var viewResult = Assert.IsAssignableFrom<IEnumerable<Product>>(result);
            Assert.NotEmpty(viewResult);
            Assert.Single(viewResult);
        }
    }
}
