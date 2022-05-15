namespace Product.Api.Data.Interfaces;

public interface IProductDbContext
{
    IMongoCollection<Entities.Product> Products { get; }
}

