using AutoFixture.Xunit2;
using Greggs.Products.Api.Currency;
using System;
using Xunit;

namespace Greggs.Products.UnitTests
{
    public class CurrencyConverterTests
    {
        [Theory]
        [InlineAutoData]
        public void RandomEurAmountConversionRateIsCorrectRateOfOnePointOneOne(decimal amountToConvert)
        {
            // Arrange
            var currencyConverter = new CurrencyConverter();

            // Act
            var convertedAmount = currencyConverter.Convert("EUR", amountToConvert);
            
            // Assert
            Assert.Equal(Math.Round(decimal.Multiply(amountToConvert, 1.11M), 2, MidpointRounding.ToEven), convertedAmount);
        }

        [Theory]
        [InlineAutoData]
        public void ExceptionIsThrownForUnknownCurrency(decimal amountToConvert, string unknownCurrency)
        {
            // Arrange
            var currencyConverter = new CurrencyConverter();

            // Act / Assert
            Assert.Throws<Exception>(() => currencyConverter.Convert(unknownCurrency, amountToConvert));
        }
    }
}
