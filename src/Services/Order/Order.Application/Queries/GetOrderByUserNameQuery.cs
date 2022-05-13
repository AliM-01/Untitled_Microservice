namespace Order.Application.Queries;

public record GetOrderByUserNameQuery(string UserName) : IRequest<IEnumerable<OrderResponseDto>>;
