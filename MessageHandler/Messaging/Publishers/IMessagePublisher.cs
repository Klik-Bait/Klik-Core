using MessageHandler.Messaging.Contracts;

namespace MessageHandler.Messaging.Publishers;

public interface IMessagePublisher
{
    Task PublishChatMessageAsync(string user, string message);
}