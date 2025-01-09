namespace MessageHandler.Messaging.Contracts;

public class ChatMessage(string sender, string message, DateTime timestamp)
{
    public string Sender { get; init; } = sender;
    public string Message { get; init; } = message;
    public DateTime Timestamp { get; init; } = timestamp;
}