using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Order.Infrastructure.Data;

public class OrderDbContextSeed
{
    public static async Task SeedAsync(OrderDbContext orderDbContext, ILoggerFactory loggerFactory, int? retry = 0)
    {
        int retryValue = retry.Value;

        try
        {
            orderDbContext.Database.Migrate();

            if (!orderDbContext.Orders.Any())
            {
                orderDbContext.Orders.AddRange(GetOrders());

                await orderDbContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            if (retryValue < 3)
            {
                retryValue++;

                var logger = loggerFactory.CreateLogger<OrderDbContextSeed>();
                logger.LogError($"OrderDbContext Seed Faild. Inner Exception : {ex.InnerException}");
                await SeedAsync(orderDbContext, loggerFactory, retryValue);
            }
        }
    }

    private static IEnumerable<Domain.Entities.Order> GetOrders()
    {
        return new List<Domain.Entities.Order>
        {
            new Domain.Entities.Order() {UserName = "abc", FirstName = "ab", LastName = "b", Email = "abc@gmail.com"}
        };
    }
}
