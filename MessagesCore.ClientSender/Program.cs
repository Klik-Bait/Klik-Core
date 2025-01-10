using MessagesCore.ClientSender.MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureMassTransit(builder.Configuration);

var app = builder.Build();

app.Run();
