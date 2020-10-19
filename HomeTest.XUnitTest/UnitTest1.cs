using HomeTest.Data;
using HomeTest.Data.Entities;
using HomeTest.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace HomeTest.XUnitTest
{
    public class UnitTest1
    {
        [Theory(DisplayName = "Add New product list")]
        [InlineData("Keyboard")]
        public async void AddProduct(string expectedName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestNewDb").Options;

            using (var context = new ApplicationDbContext(options))
            {
                var product = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Keyboard",
                    Quantity = 20,
                    Description = "",
                    Enable = true
                };

                var _productService = new EntityService<Product>(context);
                await _productService.CreateAsync(product);
            }

            using (var context = new ApplicationDbContext(options))
            {
                var _productService = new EntityService<Product>(context);
                var result = await _productService.FindByConditionAsync(p => p.Enable);

                Assert.NotEmpty(result);
                Assert.Single(result);
                Assert.NotEmpty(result.First().Name);
                Assert.Equal(expectedName, result.First().Name);
            }
        }

        [Theory(DisplayName = "Get product item")]
        [InlineData("Keyboard")]
        public async void GetProductById(string expectedName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestGetItemDb").Options;

            var id = Guid.NewGuid();

            using (var context = new ApplicationDbContext(options))
            {
                // 1. Arrange
                var product = new Product
                {
                    Id = id,
                    Name = "Keyboard",
                    Quantity = 20,
                    Description = "",
                    Enable = true
                };

                var _productService = new EntityService<Product>(context);
                await _productService.CreateAsync(product);
            }

            using (var context = new ApplicationDbContext(options))
            {
                var _productService = new EntityService<Product>(context);
                var result = await _productService.GetByIdAsync(id);

                Assert.NotNull(result);
                Assert.NotEmpty(result.Name);
                Assert.Equal(expectedName, result.Name);
            }
        }

        [Theory(DisplayName = "Update product item")]
        [InlineData("KeyboardUpdated")]
        public async void UpdateProduct(string expectedName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestUpdateItemDb").Options;

            var id = Guid.NewGuid();

            using (var context = new ApplicationDbContext(options))
            {
                var product = new Product
                {
                    Id = id,
                    Name = "Keyboard",
                    Quantity = 20,
                    Description = "",
                    Enable = true
                };

                var _productService = new EntityService<Product>(context);
                await _productService.CreateAsync(product);
            }

            using (var context = new ApplicationDbContext(options))
            {
                var productUpdated = new Product
                {
                    Id = id,
                    Name = "KeyboardUpdated",
                    Quantity = 10,
                    Description = "",
                    Enable = true
                };
                var _productService = new EntityService<Product>(context);
                var result = await _productService.UpdateAsync(productUpdated);

                Assert.NotNull(result);
                Assert.NotEmpty(result.Name);
                Assert.Equal(expectedName, result.Name);
                Assert.Equal(10, result.Quantity);
            }
        }

        [Theory(DisplayName = "Delete product item")]
        [InlineData("Keyboard")]
        public async void DeleteProduct(string expectedName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDeleteItemDb").Options;

            var id = Guid.NewGuid();
            var product = new Product
            {
                Id = id,
                Name = "Keyboard",
                Quantity = 20,
                Description = "",
                Enable = true
            };

            using (var context = new ApplicationDbContext(options))
            {
                var _productService = new EntityService<Product>(context);
                await _productService.CreateAsync(product);
            }

            using (var context = new ApplicationDbContext(options))
            {
                var _productService = new EntityService<Product>(context);
                await _productService.DisableAsync(product);
                var result = await _productService.FindByConditionAsync(p => p.Enable);
                var result1 = await _productService.GetAllAsync();

                Assert.Empty(result);
                Assert.NotEmpty(result1);
                Assert.Equal(expectedName, result1.First().Name);
                Assert.False(result1.First().Enable);
            }
        }
    }
}
