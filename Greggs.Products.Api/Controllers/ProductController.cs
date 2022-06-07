using System.Collections.Generic;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IDataAccess<Product> _productDataAccess;

    public ProductController(ILogger<ProductController> logger, IDataAccess<Product> productDataAccess)
    {
        _logger = logger;
        _productDataAccess = productDataAccess;
    }

    [HttpGet]
    public IEnumerable<Product> Get(int pageStart = 0, int pageSize = 5, string currencyCode = "GBP")
    {
        // Use structured logging - perhaps there's a sink behind the ILogger that will allow us to query the data later.
        _logger.LogDebug("Parameters: {pageStart}, {pageSize} {currencyCode}", pageStart, pageSize, currencyCode);

        return _productDataAccess
            .List(pageStart, pageSize, currencyCode);
    }
}