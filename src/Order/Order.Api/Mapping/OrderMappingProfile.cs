using AutoMapper;
using EventBusRabbitMQ.Events;
using Order.Application.DTOs;

namespace Order.Api.Mapping;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<BasketCheckoutEvent, CheckoutOrderRequestDto>().ReverseMap();
    }
}
