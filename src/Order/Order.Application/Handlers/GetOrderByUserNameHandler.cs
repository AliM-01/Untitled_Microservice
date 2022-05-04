using AutoMapper;
using Order.Application.Queries;
using Order.Domain.Repositories;

namespace Order.Application.Handlers;
public class GetOrderByUserNameHandler : IRequestHandler<GetOrderByUserNameQuery, IEnumerable<OrderResponseDto>>
{
    #region ctor

    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrderByUserNameHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    #endregion

    public async Task<IEnumerable<OrderResponseDto>> Handle(GetOrderByUserNameQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var orders = await _orderRepository.GetOrdersByUserName(request.UserName);

        return orders.Select(x => _mapper.Map<OrderResponseDto>(x));
    }
}
