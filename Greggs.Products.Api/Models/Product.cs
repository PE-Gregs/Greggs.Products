namespace Greggs.Products.Api.Models;

public class Product
{
    public string Name { get; set; }

    /// <summary>
    /// There are many ways to skin this cat (my list won't be exhaustive but here are my thoughts):
    /// - add a property for each currency E.G. PriceInXYZ and do the conversion for whether it was needed by the consumer or not.
    /// - add an array of prices. This is similar to the above but it _might_ be easier for the consumer. Perhaps there's
    ///   a config value for the currency in the consumer application - the same consumer code base could be deployed out for EUR, USD etc
    ///   by using something like product.Price[Config.CurrencyCode].
    /// - return a single price with the currency requested by the client (which I'm going to opt for here)
    /// - Sidenote for fun - could Gregs ever break the US love of their King?!
    /// </summary>
    public decimal Price { get; set; } // thumbs up for decimal for money
}