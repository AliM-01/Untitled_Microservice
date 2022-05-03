using Order.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host
    .ConfigureHostOptions(o => o.ShutdownTimeout = TimeSpan.FromSeconds(30))
    .Build()
    .CreateAndSeedDatabaseAsync();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
