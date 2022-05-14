using Catalog.Api.Data.Interfaces;
using Catalog.Api.Data.Seeds;
using Catalog.Api.Settings;
using Microsoft.Extensions.Options;

namespace Catalog.Api.Data;

public class CatalogDbContext : ICatalogDbContext
{
    #region ctor

    private readonly CatalogDbSettings _settings;
    public CatalogDbContext(IOptionsSnapshot<CatalogDbSettings> settings)
    {
        _settings = settings.Value;

        var client = new MongoClient(_settings.ConnectionString);

        var db = client.GetDatabase(_settings.DbName);

        Products = db.GetCollection<Product>(_settings.CollectionName);

        CatalogDbContextSeed.SeedDataAsync(Products).Wait();
    }

    #endregion

    public IMongoCollection<Product> Products { get; }
}

