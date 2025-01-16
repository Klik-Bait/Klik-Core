using MassTransit;
using MessageCore.Contracts;

namespace MessagesCore.ClientReceiver.Messaging.Publishers;

public class MessagePublisher(IPublishEndpoint publishEndpoint, ILogger<MessagePublisher> logger): IMessagePublisher
{
    public async Task<ChatDeliveryResponse> PublishChatMessageAsync(ChatMessage chatMessage)
    {
        var chatDelivery = new ChatDeliveryResponse(chatMessage.Receiver);

        try
        {
            await publishEndpoint.Publish(chatMessage);
            chatDelivery.IsDelivered = true;
        }
        catch (Exception e)
        {
            chatDelivery.IsDelivered = false;
            logger.LogError(e, "Error publishing chat message: {Message}", e.Message);
        }

        return chatDelivery;

    }
}