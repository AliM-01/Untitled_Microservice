namespace Order.Application.Mappings;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<Domain.Entities.Order, OrderResponseDto>().ReverseMap();

        CreateMap<CheckoutOrderRequestDto, Domain.Entities.Order>();
    }
}
