using Basket.Api.Data;
using Basket.Api.Data.Interfaces;
using Basket.Api.Repositories;
using Basket.Api.Repositories.Interfaces;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ConnectionMultiplexer>(sp =>
{
    var config = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);

    return ConnectionMultiplexer.Connect(config);
});

builder.Services.AddTransient<IBasketContext, BasketContext>();
builder.Services.AddTransient<IBasketRepository, BasketRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
