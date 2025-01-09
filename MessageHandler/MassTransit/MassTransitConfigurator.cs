using MassTransit;
using MessageHandler.Messaging.Contracts;
using MessageHandler.Settings;

namespace MessageHandler.MassTransit;

public static class MassTransitConfigurator
{
    public static IServiceCollection ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqSettings = configuration.GetSection(RabbitMqSettings.Section).Get<RabbitMqSettings>();
        if (rabbitMqSettings == null)
            throw new InvalidOperationException("RabbitMQ settings are not configured in appsettings.json.");
        rabbitMqSettings.Validate();

        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((_, cfg) =>
            {
                cfg.Host(rabbitMqSettings.Host, h =>
                {
                    h.Username(rabbitMqSettings.Username);
                    h.Password(rabbitMqSettings.Password);
                });
                
                cfg.ReceiveEndpoint("chat-queue", e =>
                {
                    e.Bind<ChatMessage>(); 
                });
                
            });
        });
        return services;
    }
}