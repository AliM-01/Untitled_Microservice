using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile("ocelot.json");
});

builder.Services.AddOcelot();

var app = builder.Build();

app.MapControllers();

await app.UseOcelot();

app.Run();
