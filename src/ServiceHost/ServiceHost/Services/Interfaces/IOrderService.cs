using ServiceHost.ViewModels.Order;

namespace ServiceHost.Services.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderViewModel>> GetOrdersByUserName(string userName);
}
