using Order.Api.RabbitMQ;

namespace Order.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static EventBusConsumer? Listener { get; set; }

    public static IApplicationBuilder UseRabbitListerner(this IApplicationBuilder app)
    {
        Listener = app.ApplicationServices.GetRequiredService<EventBusConsumer>();

        var lifecycle = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

        lifecycle.ApplicationStarted.Register(OnStarted);
        lifecycle.ApplicationStopping.Register(OnStopping);

        return app;
    }

    private static void OnStarted()
    {
        Listener?.Consume();
    }

    private static void OnStopping()
    {
        Listener?.Disconnect();
    }
}
