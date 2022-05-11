using System.Text;
using EventBusRabbitMQ.Events;
using Newtonsoft.Json;

namespace EventBusRabbitMQ.Producer;

public class EventBusProducer
{
    #region ctor

    private readonly IRabbitMQConnection _connection;

    public EventBusProducer(IRabbitMQConnection connection)
    {
        _connection = connection;
    }

    #endregion

    public void PublishBasketCheckout(string queueName, BasketCheckoutEvent publishModel)
    {
        using (var channel = _connection.CreateModel())
        {
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            string message = JsonConvert.SerializeObject(publishModel);
            byte[] body = Encoding.UTF8.GetBytes(message);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            properties.DeliveryMode = 2;

            channel.ConfirmSelect();
            channel.BasicPublish(exchange: "", routingKey: queueName, mandatory: true,
                    basicProperties: properties, body: body);
            channel.WaitForConfirmsOrDie();

            channel.BasicAcks += (sender, eventArgs) =>
            {
                Console.WriteLine("Sent RabbitMQ");
            };
            channel.ConfirmSelect();
        }
    }
}
