using Microsoft.AspNetCore.Mvc;
using ServiceHost.ViewModels.Order;

namespace ServiceHost.Pages;
public class CheckoutSuccessModel : PageModel
{
    private readonly IOrderService _orderService;

    public CheckoutSuccessModel(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public OrderViewModel Orders { get; set; }

    public async Task<IActionResult> OnGetAsync([FromQuery] string userName)
    {
        if (string.IsNullOrEmpty(userName))
            return Redirect("/");

        Orders = (await _orderService.GetOrdersByUserName(userName)).FirstOrDefault();

        return Page();
    }
}