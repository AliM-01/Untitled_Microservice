using ServiceHost.Extensions;
using ServiceHost.Services.Interfaces;
using ServiceHost.ViewModels.Product;

namespace ServiceHost.Services.Implementation;

public class ProductService : IProductService
{
    #region ctor

    private readonly HttpClient _client;

    public ProductService(IHttpClientFactory factory)
    {
        _client = factory.CreateClient("ApiGateway");
    }

    #endregion

    #region GetProduct

    public async Task<IEnumerable<ProductViewModel>> GetProduct()
    {
        var response = await _client.GetAsync("/product");
        return await response.ReadContentAs<List<ProductViewModel>>();
    }

    public async Task<ProductViewModel> GetProduct(string id)
    {
        var response = await _client.GetAsync($"/product/{id}");
        return await response.ReadContentAs<ProductViewModel>();
    }

    #endregion

    #region GetProductByCategory

    public async Task<IEnumerable<ProductViewModel>> GetProductByCategory(string category)
    {
        var response = await _client.GetAsync($"/product/category/{category}");
        return await response.ReadContentAs<List<ProductViewModel>>();
    }

    #endregion

    #region CreateProduct

    public async Task<ProductViewModel> CreateProduct(ProductViewModel product)
    {
        var response = await _client.PostAsJson($"/product", product);
        if (response.IsSuccessStatusCode)
            return await response.ReadContentAs<ProductViewModel>();
        else
        {
            throw new Exception("Something went wrong when calling api.");
        }
    }

    #endregion
}
