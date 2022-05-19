using ServiceHost.Services.Interfaces;
using ServiceHost.ViewModels.Order;

namespace ServiceHost.Services.Implementation;

public class OrderService : IOrderService
{
    #region ctor

    private readonly HttpClient _client;

    public OrderService(IHttpClientFactory factory)
    {
        _client = factory.CreateClient("ApiGateway");
    }

    #endregion

    public async Task<IEnumerable<OrderViewModel>> GetOrdersByUserName(string userName)
    {
        return await _client.GetFromJsonAsync<List<OrderViewModel>>($"/order/{userName}");
    }
}
