using Microsoft.AspNetCore.Mvc;
using ServiceHost.ViewModels.Basket;
using ServiceHost.ViewModels.Product;

namespace ServiceHost.Pages;

public class IndexModel : PageModel
{
    #region ctor

    private readonly IBasketService _basketService;
    private readonly IProductService _productService;

    public IndexModel(IProductService productService, IBasketService basketService)
    {
        _basketService = basketService;
        _productService = productService;
    }

    public IEnumerable<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();

    #endregion

    public async Task<IActionResult> OnGetAsync()
    {
        Products = await _productService.GetProducts();
        return Page();
    }

    public async Task<IActionResult> OnPostAddToCartAsync(string productId)
    {
        var product = await _productService.GetProduct(productId);

        var basket = await _basketService.GetBasket("a");

        basket.Items.Add(new BasketItemViewModel
        {
            ProductId = productId,
            ProductTitle = product.Title,
            Price = product.Price,
            Quantity = 1
        });

        await _basketService.UpdateBasket(basket);

        return RedirectToPage("Cart");
    }
}
