namespace Basket.Api.Entities;

public class BasketCartItem
{
    public int Quantity { get; set; }

    public double Price { get; set; }

    public string ProductId { get; set; }

    public string ProductTitle { get; set; }
}