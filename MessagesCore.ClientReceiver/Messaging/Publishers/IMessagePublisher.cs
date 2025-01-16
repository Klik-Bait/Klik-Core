using MessageCore.Contracts;

namespace MessagesCore.ClientReceiver.Messaging.Publishers;

public interface IMessagePublisher
{
    Task<ChatDeliveryResponse> PublishChatMessageAsync(ChatMessage chatMessage);
}