using Catalog.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.Api.Controllers;

[ApiController]
[Route("api/v1/catalog")]
public class CatalogController : ControllerBase
{
    #region ctor

    private readonly IProductRepository _productRepository;
    private readonly ILogger<CatalogController> _logger;

    public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    #endregion

    [HttpGet]
    public async Task<IActionResult> GetProduct()
    {
        var products = await _productRepository.GetAllProducts();

        return Ok(products);
    }

    [HttpGet("id/{id:length(24)}", Name = "GetProduct")]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetProductById(string id)
    {
        var product = await _productRepository.GetProductById(id);

        if (product is null)
        {
            _logger.LogError($"Product with id: {id} was not found");
            return NotFound();
        }

        return Ok(product);
    }

    [HttpGet("cat/{category}")]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetProductByCategory(string category)
    {
        var product = await _productRepository.GetProductByCategory(category);

        if (product is null)
        {
            _logger.LogError($"Product with category: {category} was not found");
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateProduct([FromBody] Product product)
    {
        await _productRepository.CreateProduct(product);

        return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateProduct([FromBody] Product product)
    {
        await _productRepository.UpdateProduct(product);

        return Ok();
    }

    [HttpDelete("delete/{id:length(24)}")]
    public async Task<IActionResult> UpdateProduct(string id)
    {
        await _productRepository.DeleteProduct(id);

        return Ok();
    }

}

