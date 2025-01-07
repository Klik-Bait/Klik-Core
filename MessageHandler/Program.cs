using MessageHandler.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

var app = builder.Build();

app.MapGet("/hc", () => "Healthy!");
app.MapGet("/health-check", () => "Healthy!");

app.MapHub<ChatHub>("/chatHub");

app.Run();