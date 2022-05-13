namespace EventBusRabbitMQ.Events;

public class BasketCheckoutEvent
{
    public Guid RequestId { get; set; } = Guid.NewGuid();

    public string UserName { get; set; }

    public decimal TotalPrice { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string CardName { get; set; }

    public string CardNumber { get; set; }

    public string CardExpr { get; set; }

    public string CVV { get; set; }

    public int PaymentMethod { get; set; }
}
