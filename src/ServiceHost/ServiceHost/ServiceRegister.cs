using Microsoft.Extensions.Diagnostics.HealthChecks;
using Polly;
using Polly.Extensions.Http;

namespace ServiceHost;

public static class ServiceRegister
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddHttpClient("ApiGateway",
                               c => c.BaseAddress = new Uri(config["ApiSettings:GatewayAddress"]))
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy()); ;

        services.AddRazorPages();

        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
            options.AppendTrailingSlash = true;
        });

        services.AddHealthChecks()
                .AddUrlGroup(new Uri(config["ApiSettings:GatewayAddress"]),
                             "Ocelot API Gateway",
                             HealthStatus.Degraded);

        return services;
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(retryAttempt),
                onRetry: (exception, retryCount, context) =>
                {
                    // TODO: Serilog
                    Console.WriteLine($"Retry {retryCount} of {context.PolicyKey} at {context.OperationKey}, due to: {exception}.");
                });
    }

    private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(
                handledEventsAllowedBeforeBreaking: 5,
                durationOfBreak: TimeSpan.FromSeconds(30)
            );
    }
}
