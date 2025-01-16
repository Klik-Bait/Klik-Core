using MessagesCore.ClientReceiver.Hubs;
using MessagesCore.ClientReceiver.MassTransit;
using MessagesCore.ClientReceiver.Messaging.Publishers;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IMessagePublisher, MessagePublisher>();
builder.Services.ConfigureMassTransit(builder.Configuration);
builder.Services.AddSignalR();

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = ConfigurationOptions.Parse("localhost:6379");
    // Adjust as needed: password, SSL, etc.
    return ConnectionMultiplexer.Connect(configuration);
});

var app = builder.Build();

app.MapGet("/hc", () => "Healthy!");

app.MapHub<ChatHub>("/chatHub");

app.Run();