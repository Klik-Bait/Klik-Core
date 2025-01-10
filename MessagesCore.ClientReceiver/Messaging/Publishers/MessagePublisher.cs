using MassTransit;
using MessageCore.Contracts;

namespace MessagesCore.ClientReceiver.Messaging.Publishers;

public class MessagePublisher(IPublishEndpoint publishEndpoint, ILogger<MessagePublisher> logger): IMessagePublisher
{
    public async Task<bool> PublishChatMessageAsync(ChatMessage chatMessage)
    {
        try
        {
            await publishEndpoint.Publish(chatMessage);
            return true;
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            return false;
        }
    }
}