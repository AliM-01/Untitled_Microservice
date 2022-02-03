using Catalog.Api.Data.Interfaces;
using Catalog.Api.Settings;

namespace Catalog.Api.Data;

public class CatalogDbContext : ICatalogDbContext
{
    #region ctor

    public CatalogDbContext(ICatalogDbSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);

        var db = client.GetDatabase(settings.DbName);

        Products = db.GetCollection<Product>(settings.CollectionName);
    }

    #endregion

    public IMongoCollection<Product> Products { get; }
}

