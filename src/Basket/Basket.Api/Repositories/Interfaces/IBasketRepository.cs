using Basket.Api.Entities;

namespace Basket.Api.Repositories.Interfaces;

public interface IBasketRepository
{
    Task<BasketCart> GetBasket(string username);

    Task<BasketCart> UpdateBasket(BasketCart cart);

    Task<bool> DeleteBasket(string username);
}
