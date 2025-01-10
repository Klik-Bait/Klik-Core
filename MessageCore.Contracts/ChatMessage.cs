namespace MessageCore.Contracts;

public class ChatMessage(string sender, string receiver ,string message, DateTime timestamp)
{
    public string Sender { get; init; } = sender;
    public string Receiver { get; init; } = receiver;
    public string Message { get; init; } = message;
    public DateTime Timestamp { get; init; } = timestamp;
}