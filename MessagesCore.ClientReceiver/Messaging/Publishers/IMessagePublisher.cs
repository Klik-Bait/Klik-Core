using MessageCore.Contracts;

namespace MessagesCore.ClientReceiver.Messaging.Publishers;

public interface IMessagePublisher
{
    Task<bool> PublishChatMessageAsync(ChatMessage chatMessage);
}