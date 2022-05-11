using System.Text;
using AutoMapper;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Events;
using MediatR;
using Newtonsoft.Json;
using Order.Application.Commands;
using Order.Application.DTOs;
using Order.Domain.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Order.Api.RabbitMQ;

public class EvenBusConsumer
{
    #region ctor

    private readonly IRabbitMQConnection _connection;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IOrderRepository _repository;

    public EvenBusConsumer(IRabbitMQConnection connection,
                           IMediator mediator,
                           IMapper mapper,
                           IOrderRepository repository)
    {
        _connection = connection;
        _mediator = mediator;
        _mapper = mapper;
        _repository = repository;
    }

    #endregion

    #region Consume

    public void Consume()
    {
        var channel = _connection.CreateModel();
        channel.QueueDeclare(queue: EventBusConstants.BasketCheckoutQueue,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += RecievedEvent;

        channel.BasicConsume(queue: EventBusConstants.BasketCheckoutQueue,
                             autoAck: true,
                             consumer: consumer,
                             noLocal: false,
                             exclusive: false,
                             arguments: null);

    }

    #endregion

    #region RecievedEvent

    private async void RecievedEvent(object sender, BasicDeliverEventArgs e)
    {
        if (e.RoutingKey.Equals(EventBusConstants.BasketCheckoutQueue))
        {
            string message = Encoding.UTF8.GetString(e.Body.Span);
            var basketCheckoutEvent = JsonConvert.DeserializeObject<BasketCheckoutEvent>(message);

            var checkout = _mapper.Map<CheckoutOrderRequestDto>(basketCheckoutEvent);

            var result = await _mediator.Send(new CheckoutOrderCommand(checkout));
        }
    }

    #endregion

    #region Disconnect

    public void Disconnect()
    {
        _connection.Dispose();
    }

    #endregion
}
