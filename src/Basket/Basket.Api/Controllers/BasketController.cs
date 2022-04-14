using AutoMapper;
using Basket.Api.Entities;
using Basket.Api.Repositories.Interfaces;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Producer;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.Api.Controllers;

[ApiController]
[Route("api/v1/basket")]
public class BasketController : ControllerBase
{
    #region ctor

    private readonly IBasketRepository _basketRepository;
    private readonly EvenBusProducer _evenBus;
    private readonly ILogger<BasketController> _logger;
    private readonly IMapper _mapper;

    public BasketController(IBasketRepository basketRepository,
                            ILogger<BasketController> logger,
                            EvenBusProducer evenBus,
                            IMapper mapper)
    {
        _basketRepository = basketRepository;
        _evenBus = evenBus;
        _logger = logger;
        _mapper = mapper;
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

    [HttpPost("checkout")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
    {
        var basket = await _basketRepository.GetBasket(basketCheckout.UserName);

        if (basket is null)
        {
            _logger.LogError("Basket is null");
            return BadRequest();
        }

        var basketToRemove = await _basketRepository.DeleteBasket(basket.UserName);

        if (!basketToRemove)
        {
            _logger.LogError("!BasketToRemove");
            return BadRequest();
        }

        var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);

        eventMessage.RequestId = Guid.NewGuid();
        eventMessage.TotalPrice = basket.TotalPrice;

        try
        {
            _evenBus.PublishBasketCheckout(EventBusConstants.BasketCheckoutQueue, eventMessage);
            return Accepted();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }
}
