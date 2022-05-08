using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.DTOs;
using Order.Application.Queries;

namespace Order.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    #region ctor

    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region GetOrdersByUserName

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OrderResponseDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetOrdersByUserName([FromQuery] string username, CancellationToken cancellationToken)
    {
        var query = new GetOrderByUserNameQuery(username);
        var orders = await _mediator.Send(query, cancellationToken);

        return Ok(orders);
    }

    #endregion


}
