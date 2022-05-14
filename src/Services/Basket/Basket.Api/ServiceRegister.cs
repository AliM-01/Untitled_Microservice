using Basket.Api.Data;
using Basket.Api.Data.Interfaces;
using Basket.Api.Data.Mappings;
using Basket.Api.Repositories;
using Basket.Api.Repositories.Interfaces;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Producer;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RabbitMQ.Client;
using StackExchange.Redis;

namespace Basket.Api;

public static class ServiceRegister
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration config)
    {
        string connectionString = config.GetConnectionString("Redis");

        services.AddSingleton<ConnectionMultiplexer>(sp =>
        {
            var config = ConfigurationOptions.Parse(connectionString, true);

            return ConnectionMultiplexer.Connect(config);
        });

        services.AddTransient<IBasketContext, BasketContext>();
        services.AddTransient<IBasketRepository, BasketRepository>();

        services.AddAutoMapper(typeof(BasketMappingProfile));

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddSingleton<IRabbitMQConnection>(sp =>
        {
            var factory = new ConnectionFactory()
            {
                HostName = config["EventBus:HostName"]
            };

            if (!string.IsNullOrEmpty(config["EventBus:UserName"]))
            {
                factory.UserName = config["EventBus:UserName"];
                factory.Password = config["EventBus:Password"];
            }

            return new RabbitMQConnection(factory);
        });

        services.AddSingleton<EventBusProducer>();

        services.AddHealthChecks()
                    .AddRedis(connectionString,
                              "Redis Health",
                              HealthStatus.Degraded);

        return services;
    }
}
