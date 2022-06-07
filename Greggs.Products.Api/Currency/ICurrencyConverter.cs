using System;

namespace Greggs.Products.Api.Currency
{
    public interface ICurrencyConverter
    {
        /// <summary>
        /// DISCLAIMER: This is only here to help enable the purpose of this exercise, this doesn't reflect the way I work!
        /// There would be a call to an external provider like open exchange rates for real time data, or a cached / stored value perhaps.
        /// I worked for a firm that worked with an insurance company and they set a fixed rate for EUR and USD for a 3 month period and took the win/hit which I found interesting.
        /// 
        /// This could be extended to take a source, target and amount if required
        /// </summary>
        decimal Convert(string targetCurrencyCode, decimal amountToConvertInGbp);
    }
}
