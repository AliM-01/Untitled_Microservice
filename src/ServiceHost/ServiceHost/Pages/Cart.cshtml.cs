using Microsoft.AspNetCore.Mvc;
using ServiceHost.ViewModels.Basket;

namespace ServiceHost.Pages;

public class CartModel : PageModel
{
    private readonly IBasketService _basketService;

    public CartModel(IBasketService basketService)
    {
        _basketService = basketService;
    }

    public BasketViewModel Cart { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        string userName = "a";
        Cart = await _basketService.GetBasket(userName);

        return Page();
    }

    public async Task<IActionResult> OnPostRemoveFromCartAsync(string productId)
    {
        string userName = "a";
        var basket = await _basketService.GetBasket(userName);

        var item = basket.Items.Single(x => x.ProductId == productId); 
        basket.Items.Remove(item);

        var basketUpdated = await _basketService.UpdateBasket(basket);

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostIncreaseAsync(string productId)
    {
        string userName = "a";
        var basket = await _basketService.GetBasket(userName);

        var item = basket.Items.Single(x => x.ProductId == productId);
        basket.Items.Remove(item);

        item.Quantity += 1;

        basket.Items.Add(item);

        await _basketService.UpdateBasket(basket);

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostReduceAsync(string productId)
    {
        string userName = "a";
        var basket = await _basketService.GetBasket(userName);

        var item = basket.Items.Single(x => x.ProductId == productId);

        if (item.Quantity > 1)
        {
            item.Quantity -= 1;
            basket.Items.Remove(item);
            basket.Items.Add(item);
        }
        else
        {
            basket.Items.Remove(item);
        }

        await _basketService.UpdateBasket(basket);

        return RedirectToPage();
    }
}
