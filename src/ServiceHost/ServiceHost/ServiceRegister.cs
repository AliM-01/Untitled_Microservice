namespace ServiceHost;

public static class ServiceRegister
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddHttpClient("ApiGateway",
                               c => c.BaseAddress = new Uri(config["ApiSettings:GatewayAddress"]));

        services.AddRazorPages();

        return services;
    }
}
