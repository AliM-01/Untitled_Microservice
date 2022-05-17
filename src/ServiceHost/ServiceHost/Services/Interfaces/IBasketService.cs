using ServiceHost.ViewModels.Basket;

namespace ServiceHost.Services.Interfaces;

public interface IBasketService
{
    Task<BasketViewModel> GetBasket(string userName);
    Task<BasketViewModel> UpdateBasket(BasketViewModel basket);
    Task CheckoutBasket(BasketCheckoutViewModel basketCheckout);
}
