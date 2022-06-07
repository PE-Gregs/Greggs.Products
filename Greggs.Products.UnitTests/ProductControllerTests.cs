using AutoFixture;
using AutoFixture.Xunit2;
using Greggs.Products.Api.Controllers;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Greggs.Products.UnitTests
{
    public class ProductControllerTests
    {
        private readonly Fixture _fixture;

        public ProductControllerTests()
        {
            _fixture = new Fixture();
        }

        [Theory]
        [InlineAutoData]
        public void GetCallsDataAccessWithCorrectParameters(int pageStart, int pageSize)
        {
            // Arrange
            var mockProductDataAccess = new Mock<IDataAccess<Product>>();
            mockProductDataAccess.Setup(x => x.List(pageStart, pageSize, "GBP")).Returns(new List<Product>());

            var controller = new ProductController(new Mock<ILogger<ProductController>>().Object, mockProductDataAccess.Object);

            // Act
            controller.Get(pageStart, pageSize);

            // Assert
            mockProductDataAccess.Verify(x => x.List(pageStart, pageSize, "GBP"), Times.Once());
        }

        [Fact]
        public void GetReturnsDataFromDataAccess()
        {
            // Arrange
            var testReturnProductData = _fixture.CreateMany<Product>().ToList();
            var mockProductDataAccess = new Mock<IDataAccess<Product>>();
            mockProductDataAccess.Setup(x => x.List(It.IsAny<int>(), It.IsAny<int>(), "GBP")).Returns(testReturnProductData);

            var controller = new ProductController(new Mock<ILogger<ProductController>>().Object, mockProductDataAccess.Object);

            // Act
            var productData = controller.Get();

            // Assert
            Assert.Equal(testReturnProductData, productData);
        }
    }
}
