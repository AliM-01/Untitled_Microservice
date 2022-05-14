using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;

namespace Order.Infrastructure.Data;

public class OrderDbContextSeed
{
    private const int MAX_RETRIES = 3;

    public static async Task SeedAsync(OrderDbContext orderDbContext, ILoggerFactory loggerFactory)
    {
        var logger = loggerFactory.CreateLogger<OrderDbContextSeed>();
        logger.LogInformation($"OrderDbContext Seed Started");

        var retryPolicy = Policy.Handle<Exception>()
                    .WaitAndRetryAsync(
                       retryCount: MAX_RETRIES,
                       sleepDurationProvider: times => TimeSpan.FromSeconds(times),
                       onRetry: (exception, sleepDuration, attemptNumber, context) =>
                       {
                           logger.LogError("OrderDbContext Seed error. Retrying in {0}. {1}/{2}", sleepDuration, attemptNumber, MAX_RETRIES);
                       });

        await retryPolicy.ExecuteAsync(async () =>
        {
            await orderDbContext.Database.MigrateAsync();

            if (!(await orderDbContext.Orders.AnyAsync()))
            {
                await orderDbContext.Orders.AddRangeAsync(GetOrders());

                await orderDbContext.SaveChangesAsync();
            }
        });
    }

    private static IEnumerable<Domain.Entities.Order> GetOrders()
    {
        return new List<Domain.Entities.Order>
        {
            new Domain.Entities.Order() {UserName = "abc", FirstName = "ab", LastName = "b", Email = "abc@gmail.com"}
        };
    }
}
