namespace Catalog.Api.Data.Interfaces;

public interface ICatalogDbContext
{
    IMongoCollection<Product> Products { get; }
}

