namespace MessageHandler.Settings;

public class RabbitMqSettings
{
    public const string Section = "RabbitMq";
    public required string Host { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
    
    public void Validate()
    {
        if (string.IsNullOrEmpty(Host))
            throw new ArgumentNullException(nameof(Host), "RabbitMQ Host is required.");

        if (string.IsNullOrEmpty(Username))
            throw new ArgumentNullException(nameof(Username), "RabbitMQ Username is required.");

        if (string.IsNullOrEmpty(Password))
            throw new ArgumentNullException(nameof(Password), "RabbitMQ Password is required.");
    }
}