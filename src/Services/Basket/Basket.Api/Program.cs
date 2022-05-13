using Basket.Api.Data;
using Basket.Api.Data.Interfaces;
using Basket.Api.Data.Mappings;
using Basket.Api.Repositories;
using Basket.Api.Repositories.Interfaces;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Producer;
using RabbitMQ.Client;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

#region configure services

builder.Services.AddSingleton<ConnectionMultiplexer>(sp =>
{
    var config = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);

    return ConnectionMultiplexer.Connect(config);
});

builder.Services.AddTransient<IBasketContext, BasketContext>();
builder.Services.AddTransient<IBasketRepository, BasketRepository>();

builder.Services.AddAutoMapper(typeof(BasketMappingProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region RabbitMQ

builder.Services.AddSingleton<IRabbitMQConnection>(sp =>
{
    var factory = new ConnectionFactory()
    {
        HostName = builder.Configuration["EventBus:HostName"]
    };

    if (!string.IsNullOrEmpty(builder.Configuration["EventBus:UserName"]))
    {
        factory.UserName = builder.Configuration["EventBus:UserName"];
        factory.Password = builder.Configuration["EventBus:Password"];
    }

    return new RabbitMQConnection(factory);
});

builder.Services.AddSingleton<EventBusProducer>();

#endregion

#endregion

#region configure

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion
