using Microsoft.AspNetCore.Mvc;
using ServiceHost.ViewModels.Product;

namespace ServiceHost.Pages;

public class ProductsModel : PageModel
{
    private readonly IProductService _productService;

    public ProductsModel(IProductService productService)
    {
        _productService = productService;

        Show = 10;
    }

    public IEnumerable<string> CategoryList { get; set; } = new List<string>();
    public IEnumerable<ProductViewModel> ProductList { get; set; } = new List<ProductViewModel>();

    [BindProperty(SupportsGet = true)]
    public string SelectedCategory { get; set; }

    [BindProperty(SupportsGet = true)]
    public int Show { get; set; }

    public async Task<IActionResult> OnGetAsync(string categoryName)
    {
        var productList = await _productService.GetProducts();

        CategoryList = productList.Select(p => p.Category).Distinct();

        if (!string.IsNullOrWhiteSpace(categoryName))
        {
            ProductList = productList.Where(p => p.Category == categoryName).Take(Show);
            SelectedCategory = categoryName;
        }
        else
            ProductList = productList.Take(Show);

        return Page();
    }
}
