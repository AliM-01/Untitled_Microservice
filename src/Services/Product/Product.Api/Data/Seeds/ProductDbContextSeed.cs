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
                Title = "iPhone 13",
                Price = new Random().Next(750, 1200),
                Category = "Smart Phone",
                Summary = "Lorem ipsum dolor sit amet, consectetur adipiscing",
                Description = description,
                ImagePath = "c356912346ad44a1bb6e05ac4fcd60f7.png"
            },
            new Entities.Product
            {
                Title = "iPhone 12",
                Price = new Random().Next(750, 1200),
                Category = "Smart Phone",
                Summary = "Lorem ipsum dolor sit amet, consectetur adipiscing",
                Description = description,
                ImagePath = "93081292877443cea88c44dcf8ded546.png"
            },
            new Entities.Product
            {
                Title = "iPhone SE",
                Price = new Random().Next(750, 1200),
                Category = "Smart Phone",
                Summary = "Lorem ipsum dolor sit amet, consectetur adipiscing",
                Description = description,
                ImagePath = "e031f20db3b745218065b144a4595a92.png"
            },
            new Entities.Product
            {
                Title = "iPhone 11",
                Price = new Random().Next(750, 1200),
                Category = "Smart Phone",
                Summary = "Lorem ipsum dolor sit amet, consectetur adipiscing",
                Description = description,
                ImagePath = "853990cf3e1044429dfbd994a428d786.png"
            }
        };
    }
}