using MassTransit;

namespace MessagesCore.ClientReceiver.MassTransit;

public static class MassTransitConfigurator
{
    public static IServiceCollection ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqSettings = configuration.GetSection(RabbitMqSettings.Section).Get<RabbitMqSettings>()
            ?? throw new InvalidOperationException("RabbitMQ settings are not configured in appsettings.json.");
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
                
                cfg.UseMessageRetry(r =>
                {
                    r.Interval(3, TimeSpan.FromSeconds(10));
                });
            });
        });
        return services;
    }
}