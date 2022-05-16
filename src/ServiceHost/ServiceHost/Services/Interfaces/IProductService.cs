namespace ServiceHost.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductModel>> GetProduct();
    Task<IEnumerable<ProductModel>> GetProductByCategory(string category);
    Task<ProductModel> GetProduct(string id);
    Task<ProductModel> CreateProduct(ProductModel model);
}
