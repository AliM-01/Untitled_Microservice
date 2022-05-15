namespace Product.Api.Repositories.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Entities.Product>> GetAllProducts();

    Task<Entities.Product> GetProductById(string id);

    Task<IEnumerable<Entities.Product>> GetProductByName(string title);

    Task<IEnumerable<Entities.Product>> GetProductByCategory(string categoryName);

    Task CreateProduct(Entities.Product product);

    Task<bool> UpdateProduct(Entities.Product product);

    Task<bool> DeleteProduct(string id);
}
