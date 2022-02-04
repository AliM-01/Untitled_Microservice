using Catalog.Api.Data;
using Catalog.Api.Data.Interfaces;
using Catalog.Api.Repositories;
using Catalog.Api.Repositories.Interfaces;
using Catalog.Api.Settings;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

builder.Services.Configure<CatalogDbSettings>(configuration.GetSection("CatalogDbSettings"));

builder.Services.AddSingleton<ICatalogDbSettings>(sp => sp.GetRequiredService<CatalogDbSettings>());

builder.Services.AddTransient<ICatalogDbContext, CatalogDbContext>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();

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
