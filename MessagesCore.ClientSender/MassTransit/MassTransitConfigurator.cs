using MassTransit;
using MessagesCore.ClientSender.Consumers;

namespace MessagesCore.ClientSender.MassTransit;

public static class MassTransitConfigurator
{
    public static IServiceCollection ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqSettings = configuration.GetSection(RabbitMqSettings.Section).Get<RabbitMqSettings>()
            ?? throw new InvalidOperationException("RabbitMQ settings are not configured in appsettings.json.");
        rabbitMqSettings.Validate();

        services.AddMassTransit(x =>
        {
            x.AddConsumer<ChatMessageConsumer>();
            
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitMqSettings.Host, h =>
                {
                    h.Username(rabbitMqSettings.Username);
                    h.Password(rabbitMqSettings.Password);
                });
                
                cfg.ReceiveEndpoint("chat-queue", e =>
                {
                    e.ConfigureConsumer<ChatMessageConsumer>(context);
                });
            });
        });
        return services;
    }
}