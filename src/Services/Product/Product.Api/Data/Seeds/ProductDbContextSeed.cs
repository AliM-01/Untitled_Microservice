using Polly;

namespace Product.Api.Data.Seeds;

public static class ProductDbContextSeed
{
    public static async Task SeedDataAsync(IMongoCollection<Entities.Product> products)
    {
        var retryPolicy = Policy.Handle<Exception>()
                    .WaitAndRetryAsync(
                       retryCount: 3,
                       sleepDurationProvider: times => TimeSpan.FromSeconds(times));

        await retryPolicy.ExecuteAsync(async () =>
        {
            bool existsProduct = products.Find(_ => true).Any();

            if (!existsProduct)
                await products.InsertManyAsync(GetProducts());

        });
    }

    private static IEnumerable<Entities.Product> GetProducts()
    {
        const string description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua";

        return new List<Entities.Product>
        {
            new Entities.Product
            {
                Title = "IPHONE 13",
                Price = new Random().Next(750, 1200),
                Category = "Smart Phone",
                Summary = "Lorem ipsum dolor sit amet, consectetur adipiscing",
                Description = description,
                ImagePath = "iphone_13.jpg"
            },
            new Entities.Product
            {
                Title = "IPHONE 12",
                Price = new Random().Next(750, 1200),
                Category = "Smart Phone",
                Summary = "Lorem ipsum dolor sit amet, consectetur adipiscing",
                Description = description,
                ImagePath = "iphone_12.jpg"
            },
            new Entities.Product
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