using Microsoft.AspNetCore.Mvc;
using ServiceHost.ViewModels.Product;

namespace ServiceHost.Pages;

public class IndexModel : PageModel
{
    #region ctor

    private readonly IProductService _productService;

    public IndexModel(IProductService productService)
    {
        _productService = productService;
    }

    public IEnumerable<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();

    #endregion

    public async Task<IActionResult> OnGetAsync()
    {
        Products = await _productService.GetProducts();
        return Page();
    }
}
