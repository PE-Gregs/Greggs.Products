using System;

namespace Greggs.Products.Api.Currency
{
    public class CurrencyConverter : ICurrencyConverter
    {
        public decimal Convert(string targetCurrencyCode, decimal amountToConvertInGbp)
        {
            return targetCurrencyCode.ToUpperInvariant() switch
            {
                // I chose to explicitly set MidpointRounding.ToEven because I thought this is a financial calculation and any rounding bias should be removed.
                "EUR" => Math.Round(decimal.Multiply(amountToConvertInGbp, 1.11M), 2, MidpointRounding.ToEven),
                _ => throw new Exception($"Unknown {nameof(targetCurrencyCode)}: {targetCurrencyCode}"),
            };
        }
    }
}
