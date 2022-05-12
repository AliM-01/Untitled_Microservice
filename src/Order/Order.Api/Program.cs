using System.Reflection;
using EventBusRabbitMQ;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Api.Extensions;
using Order.Api.RabbitMQ;
using Order.Application;
using Order.Domain.Repositories;
using Order.Domain.Repositories.Base;
using Order.Infrastructure.Data;
using Order.Infrastructure.Repositories;
using Order.Infrastructure.Repositories.Base;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrderDb")), ServiceLifetime.Singleton);

builder.Services.CreateAndSeedDatabaseAsync();

builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
builder.Services.AddTransient<IOrderRepository, OrderRepository>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddMediatR(typeof(IOrderApplicationAssemblyMarker).GetTypeInfo().Assembly);

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

builder.Services.AddSingleton<EventBusConsumer>();

#endregion

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRabbitListerner();

app.Run();
