using ServiceHost.Services.Extensions;
using ServiceHost.Services.Interfaces;
using ServiceHost.ViewModels.Basket;

namespace ServiceHost.Services.Implementation;

public class BasketService : IBasketService
{
    #region ctor

    private readonly HttpClient _client;

    public BasketService(IHttpClientFactory factory)
    {
        _client = factory.CreateClient("ApiGateway");
    }

    #endregion

    public async Task<BasketViewModel> GetBasket(string userName)
    {
        return await _client.GetFromJsonAsync<BasketViewModel>($"/basket/{userName}");
    }

    public async Task<BasketViewModel> UpdateBasket(BasketViewModel basket)
    {
        var response = await _client.PostAsJsonAsync("/basket", basket);
        response.EnsureSuccessStatusCode();

        return await response.ReadAsAsync<BasketViewModel>();
    }

    public async Task CheckoutBasket(BasketCheckoutViewModel basketCheckout)
    {
        var response = await _client.PostAsJsonAsync("/basket/checkout", basketCheckout);
        response.EnsureSuccessStatusCode();
    }
}
