using Greggs.Products.Api.Currency;
using Greggs.Products.Api.DataAccess;
using Moq;
using Xunit;

namespace Greggs.Products.UnitTests
{
    public class ProductAccessTests
    {
        [Fact]
        public void ListCallsCurrencyConverterWhenAppropriate()
        {
            // Arrange
            var currencycode = "Eur";
            var mockCurrencyConverter = new Mock<ICurrencyConverter>();
            ProductAccess productAccess = new ProductAccess(mockCurrencyConverter.Object);

            // Act
            var productData = productAccess.List(0, 2, currencycode);

            // Assert
            mockCurrencyConverter.Verify(x => x.Convert(currencycode, It.IsAny<decimal>()), Times.Exactly(2));
        }

        [Fact]
        public void ListDoesNotCallCurrencyConverterWhenInappropriate()
        {
            // Arrange
            var currencycode = "GBP";
            var mockCurrencyConverter = new Mock<ICurrencyConverter>();
            ProductAccess productAccess = new ProductAccess(mockCurrencyConverter.Object);

            // Act
            var productData = productAccess.List(0, 2, currencycode);

            // Assert
            mockCurrencyConverter.Verify(x => x.Convert(currencycode, It.IsAny<decimal>()), Times.Never());
        }
    }
}
