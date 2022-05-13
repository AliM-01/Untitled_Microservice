using Order.Application.Commands;
using Order.Domain.Repositories;

namespace Order.Application.Handlers;
public class CheckoutOrderHandler : IRequestHandler<CheckoutOrderCommand, OrderResponseDto>
{
    #region ctor

    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public CheckoutOrderHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    #endregion

    public async Task<OrderResponseDto> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var order = _mapper.Map<Domain.Entities.Order>(request.Checkout);

        var createResponse = await _orderRepository.AddAsync(order);

        return _mapper.Map<OrderResponseDto>(createResponse);
    }
}
