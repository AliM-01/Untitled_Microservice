namespace ServiceHost.ViewModels.Basket;

public class BasketViewModel
{
    public string UserName { get; set; }

    public List<BasketItemViewModel> Items { get; set; } = new List<BasketItemViewModel>();

    public decimal TotalPrice { get; set; }

}
