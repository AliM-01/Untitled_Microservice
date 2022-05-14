using Polly;

namespace Catalog.Api.Data.Seeds;

public class CatalogDbContextSeed
{
    private const int MAX_RETRIES = 3;

    public static async Task SeedDataAsync(IMongoCollection<Product> products)
    {
        var retryPolicy = Policy.Handle<Exception>()
                    .WaitAndRetryAsync(
                       retryCount: MAX_RETRIES,
                       sleepDurationProvider: times => TimeSpan.FromSeconds(times));

        await retryPolicy.ExecuteAsync(async () =>
        {
            bool existsProduct = products.Find(_ => true).Any();

            if (!existsProduct)
                await products.InsertManyAsync(GetProducts());

        });
    }

    private static List<Product> GetProducts()
    {
        string description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua";

        return new()
        {
            new Product
            {
                Title = "IPHONE 13",
                Price = new Random().Next(750, 1200),
                Category = "Smart Phone",
                Summary = "Lorem ipsum dolor sit amet, consectetur adipiscing",
                Description = description,
                ImagePath = "iphone_13.jpg"
            },
            new Product
            {
                Title = "IPHONE 12",
                Price = new Random().Next(750, 1200),
                Category = "Smart Phone",
                Summary = "Lorem ipsum dolor sit amet, consectetur adipiscing",
                Description = description,
                ImagePath = "iphone_12.jpg"
            },
            new Product
            {
                Title = "IPHONE 11",
                Price = new Random().Next(750, 1200),
                Category = "Smart Phone",
                Summary = "Lorem ipsum dolor sit amet, consectetur adipiscing",
                Description = description,
                ImagePath = "iphone_11.jpg"
            }
        };
    }
}