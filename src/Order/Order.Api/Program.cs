using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Api.Extensions;
using Order.Application;
using Order.Domain.Repositories;
using Order.Domain.Repositories.Base;
using Order.Infrastructure.Data;
using Order.Infrastructure.Repositories;
using Order.Infrastructure.Repositories.Base;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrderDb")), ServiceLifetime.Singleton);

builder.Services.CreateAndSeedDatabaseAsync();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddMediatR(typeof(IOrderApplicationAssemblyMarker).GetTypeInfo().Assembly);

builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
