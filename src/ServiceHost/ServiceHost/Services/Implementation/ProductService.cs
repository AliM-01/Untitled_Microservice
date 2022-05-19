using ServiceHost.Services.Extensions;
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

    public async Task<IEnumerable<ProductViewModel>> GetProducts()
    {
        return await _client.GetFromJsonAsync<List<ProductViewModel>>("/product");
    }

    public async Task<ProductViewModel> GetProduct(string id)
    {
        return await _client.GetFromJsonAsync<ProductViewModel>($"/product/{id}");
    }

    #endregion

    #region GetProductByCategory

    public async Task<IEnumerable<ProductViewModel>> GetProductByCategory(string category)
    {
        return await _client.GetFromJsonAsync<List<ProductViewModel>>($"/product/category/{category}");
    }

    #endregion

    #region CreateProduct

    public async Task<ProductViewModel> CreateProduct(ProductViewModel product)
    {
        var response = await _client.PostAsJsonAsync($"/product", product);
        if (response.IsSuccessStatusCode)
            return await response.ReadAsAsync<ProductViewModel>();
        else
            throw new Exception("Something went wrong when calling api.");
    }

    #endregion
}
