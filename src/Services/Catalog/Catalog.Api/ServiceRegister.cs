using Catalog.Api.Data;
using Catalog.Api.Data.Interfaces;
using Catalog.Api.Repositories;
using Catalog.Api.Repositories.Interfaces;
using Catalog.Api.Settings;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Catalog.Api;

public static class ServiceRegister
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration config)
    {
        var catalogDbConfig = (CatalogDbSettings)config.GetSection("CatalogDbSettings").Get(typeof(CatalogDbSettings));

        services.Configure<CatalogDbSettings>(config.GetSection("CatalogDbSettings"));

        services.AddOptions();

        services.AddHealthChecks()
                    .AddMongoDb(catalogDbConfig.ConnectionString,
                                "MongoDb Health",
                                HealthStatus.Degraded);

        services.AddTransient<ICatalogDbContext, CatalogDbContext>();
        services.AddTransient<IProductRepository, ProductRepository>();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}
