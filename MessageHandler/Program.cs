using MessageHandler.Hubs;
using MessageHandler.MassTransit;
using MessageHandler.Messaging.Publishers;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IMessagePublisher, MessagePublisher>();
builder.Services.ConfigureMassTransit(builder.Configuration);
builder.Services.AddSignalR();

var app = builder.Build();

app.MapGet("/hc", () => "Healthy!");
app.MapGet("/health", () => "Healthy!");
app.MapGet("/health-check", () => "Healthy!");

app.MapHub<ChatHub>("/chatHub");

app.Run();