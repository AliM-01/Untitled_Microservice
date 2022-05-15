using Product.Api.Data;
using Product.Api.Data.Interfaces;
using Product.Api.Repositories;
using Product.Api.Repositories.Interfaces;
using Product.Api.Settings;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Product.Api;

public static class ServiceRegister
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration config)
    {
        var ProductDbConfig = (ProductDbSettings)config.GetSection("ProductDbSettings").Get(typeof(ProductDbSettings));

        services.Configure<ProductDbSettings>(config.GetSection("ProductDbSettings"));

        services.AddOptions();

        services.AddHealthChecks()
                    .AddMongoDb(ProductDbConfig.ConnectionString,
                                "MongoDb Health",
                                HealthStatus.Degraded);

        services.AddTransient<IProductDbContext, ProductDbContext>();
        services.AddTransient<IProductRepository, ProductRepository>();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}
