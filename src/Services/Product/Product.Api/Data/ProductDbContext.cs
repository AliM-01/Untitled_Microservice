using Product.Api.Data.Interfaces;
using Product.Api.Data.Seeds;
using Product.Api.Settings;
using Microsoft.Extensions.Options;

namespace Product.Api.Data;

public class ProductDbContext : IProductDbContext
{
    #region ctor

    private readonly ProductDbSettings _settings;
    public ProductDbContext(IOptionsSnapshot<ProductDbSettings> settings)
    {
        _settings = settings.Value;

        var client = new MongoClient(_settings.ConnectionString);

        var db = client.GetDatabase(_settings.DbName);

        Products = db.GetCollection<Entities.Product>(_settings.CollectionName);

        ProductDbContextSeed.SeedDataAsync(Products).Wait();
    }

    #endregion

    public IMongoCollection<Entities.Product> Products { get; }
}

