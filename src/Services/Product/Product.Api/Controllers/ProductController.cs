using System.Net;
using Microsoft.AspNetCore.Mvc;
using Product.Api.Repositories.Interfaces;

namespace Product.Api.Controllers;

[ApiController]
[Route("api/v1/product")]
public class ProductController : ControllerBase
{
    #region ctor

    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductController> _logger;

    public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
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

    [HttpGet("{id:length(24)}", Name = "GetProduct")]
    [ProducesResponseType(typeof(Entities.Product), (int)HttpStatusCode.OK)]
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

    [HttpGet("/category/{category}")]
    [ProducesResponseType(typeof(Entities.Product), (int)HttpStatusCode.OK)]
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

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] Entities.Product product)
    {
        await _productRepository.CreateProduct(product);

        return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody] Entities.Product product)
    {
        await _productRepository.UpdateProduct(product);

        return Ok();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> UpdateProduct(string id)
    {
        await _productRepository.DeleteProduct(id);

        return Ok();
    }

}

