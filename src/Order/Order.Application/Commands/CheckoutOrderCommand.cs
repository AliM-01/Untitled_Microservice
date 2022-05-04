namespace Order.Application.Commands;

public record CheckoutOrderCommand(CheckoutOrderRequestDto Checkout) : IRequest<OrderResponseDto>;

