using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.Currency;
using Greggs.Products.Api.Models;

namespace Greggs.Products.Api.DataAccess;

/// <summary>
/// DISCLAIMER: This is only here to help enable the purpose of this exercise, this doesn't reflect the way we work!
/// </summary>
public class ProductAccess : IDataAccess<Product>
{
    private readonly ICurrencyConverter _currencyConverter;

    public ProductAccess(ICurrencyConverter currencyConverter)
    {
        _currencyConverter = currencyConverter;
    }

    // I removed the static here to prevent having to clone for the demo.
    // I am assuming that this would come from a db/cache/web api which would be materialised and would not be modified by ref during the currency conversion.
    private readonly IEnumerable<Product> ProductDatabase = new List<Product>()
    {
        new() { Name = "Sausage Roll", Price = 1m },
        new() { Name = "Vegan Sausage Roll", Price = 1.1m },
        new() { Name = "Steak Bake", Price = 1.2m },
        new() { Name = "Yum Yum", Price = 0.7m },
        new() { Name = "Pink Jammie", Price = 0.5m },
        new() { Name = "Mexican Baguette", Price = 2.1m },
        new() { Name = "Bacon Sandwich", Price = 1.95m },
        new() { Name = "Coca Cola", Price = 1.2m }
    };

    // In practice the signature would be changed to an async Task<IEnumerable<Product>> and callers marked as async and awaiting this call.
    public IEnumerable<Product> List(int? pageStart, int? pageSize, string currencyCode)
    {
        var queryable = ProductDatabase.AsQueryable();

        if (pageStart.HasValue)
            queryable = queryable.Skip(pageStart.Value);

        if (pageSize.HasValue)
            queryable = queryable.Take(pageSize.Value);

        var products = queryable.ToList();

        if (!string.Equals(currencyCode, "GBP", System.StringComparison.InvariantCultureIgnoreCase))
        {
            products.ForEach(x => x.Price = _currencyConverter.Convert(currencyCode, x.Price));
        }

        return products;
    }
}