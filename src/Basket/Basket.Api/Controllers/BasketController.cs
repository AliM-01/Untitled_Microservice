using Basket.Api.Entities;
using Basket.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.Api.Controllers;

[ApiController]
[Route("api/v1/basket")]
public class BasketController : ControllerBase
{
    #region ctor

    private readonly IBasketRepository _basketRepository;
    private readonly ILogger<BasketController> _logger;

    public BasketController(IBasketRepository basketRepository, ILogger<BasketController> logger)
    {
        _basketRepository = basketRepository;
        _logger = logger;
    }

    #endregion

    [HttpGet]
    [ProducesResponseType(typeof(BasketCart), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBasket(string username)
    {
        return Ok(await _basketRepository.GetBasket(username));
    }

    [HttpPost]
    [ProducesResponseType(typeof(BasketCart), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateBasket([FromBody] BasketCart cart)
    {
        return Ok(await _basketRepository.UpdateBasket(cart));
    }

    [HttpDelete("{username}")]
    public async Task<IActionResult> DeleteBasket(string username)
    {
        await _basketRepository.DeleteBasket(username);
        return Ok();
    }
}
