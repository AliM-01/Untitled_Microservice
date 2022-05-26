namespace Basket.Api.Entities;

public class BasketCart
{
    public string UserName { get; set; }

    public List<BasketCartItem> Items { get; set; } = new List<BasketCartItem>();

    public BasketCart()
    {
    }

    public BasketCart(string username)
    {
        UserName = username;
    }

    public double TotalPrice
    {
        get
        {
            double total = 0;

            foreach (var item in Items)
            {
                total += item.Price * item.Quantity;
            }

            return total;
        }
    }
}
