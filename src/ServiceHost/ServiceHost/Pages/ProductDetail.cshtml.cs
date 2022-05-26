using Microsoft.AspNetCore.Mvc;
using ServiceHost.ViewModels.Product;

namespace ServiceHost.Pages;

public class ProductDetailModel : PageModel
{
    private readonly IProductService _productService;
    private readonly IBasketService _basketService;

    public ProductDetailModel(IProductService productService, IBasketService basketService)
    {
        _productService = productService;
        _basketService = basketService;
    }

    public ProductViewModel Product { get; set; }

    [BindProperty]
    public int Quantity { get; set; }

    public async Task<IActionResult> OnGetAsync(string productId)
    {
        if (productId == null)
        {
            return NotFound();
        }

        Product = await _productService.GetProduct(productId);

        if (Product is null)
            return NotFound();

        Product.ImagePath = await _productService.GetProductImageUri(Product.ImagePath);

        return Page();
    }

    public async Task<IActionResult> OnPostAddToCartAsync(string productId)
    {
        var product = await _productService.GetProduct(productId);

        string userName = "a";
        var basket = await _basketService.GetBasket(userName);

        basket.Items.Add(new ViewModels.Basket.BasketItemViewModel
        {
            ProductId = productId,
            ProductTitle = product.Title,
            Price = product.Price,
            Quantity = Quantity
        });

        await _basketService.UpdateBasket(basket);

        return RedirectToPage("Cart");
    }
}

