using Order.Domain.Repositories.Base;

namespace Order.Domain.Repositories;
public interface IOrderRepository : IRepository<Entities.Order>
{
    Task<IEnumerable<Entities.Order>> GetOrdersByUserName(string userName);
}
