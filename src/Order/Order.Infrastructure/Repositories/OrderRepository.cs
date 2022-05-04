using Microsoft.EntityFrameworkCore;
using Order.Domain.Repositories;
using Order.Infrastructure.Data;
using Order.Infrastructure.Repositories.Base;

namespace Order.Infrastructure.Repositories;

public class OrderRepository : BaseRepository<Domain.Entities.Order>, IOrderRepository
{
    #region ctor

    public OrderRepository(OrderDbContext dbContext) : base(dbContext)
    {
    }

    #endregion

    #region GetOrderByUserName

    public async Task<IEnumerable<Domain.Entities.Order>> GetOrdersByUserName(string userName)
    {
        return await _dbContext.Orders
            .Where(u => EF.Functions.Like(u.UserName, $"%{userName}%"))
            .ToListAsync();
    }

    #endregion
}
