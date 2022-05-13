namespace Catalog.Api.Repositories.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProducts();

    Task<Product> GetProductById(string id);

    Task<IEnumerable<Product>> GetProductByName(string title);

    Task<IEnumerable<Product>> GetProductByCategory(string categoryName);

    Task CreateProduct(Product product);

    Task<bool> UpdateProduct(Product product);

    Task<bool> DeleteProduct(string id);
}
