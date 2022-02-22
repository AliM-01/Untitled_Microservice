using AutoMapper;
using Basket.Api.Entities;
using EventBusRabbitMQ.Events;

namespace Basket.Api.Data.Mappings;

public class BasketMappingProfile : Profile
{
    public BasketMappingProfile()
    {
        CreateMap<BasketCart, BasketCheckoutEvent>();
    }
}
