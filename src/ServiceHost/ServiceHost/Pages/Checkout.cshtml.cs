using Microsoft.AspNetCore.Mvc;
using ServiceHost.ViewModels.Basket;

namespace ServiceHost.Pages;
public class CheckoutModel : PageModel
{
    private readonly IBasketService _basketService;

    public CheckoutModel(IBasketService basketService)
    {
        _basketService = basketService;
    }

    [BindProperty]
    public BasketCheckoutViewModel Order { get; set; }

    public BasketViewModel Cart { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        string userName = "a";
        Cart = await _basketService.GetBasket(userName);

        return Page();
    }

    public async Task<IActionResult> OnPostCheckoutAsync()
    {
        string userName = "a";
        Cart = await _basketService.GetBasket(userName);

        if (!ModelState.IsValid)
        {
            return Page();
        }

        Order.UserName = userName;
        Order.TotalPrice = Cart.TotalPrice;

        await _basketService.CheckoutBasket(Order);

        return RedirectToPage("CheckoutSuccess", new { u = userName });
    }
}
