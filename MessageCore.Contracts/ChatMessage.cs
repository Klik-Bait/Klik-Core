namespace MessageCore.Contracts;

public class ChatMessage(User sender, User receiver, string message, DateTime timestamp)
{
    public User Sender { get; init; } = sender;
    public User Receiver { get; init; } = receiver;
    public string Message { get; init; } = message;
    public DateTime Timestamp { get; init; } = timestamp;
}