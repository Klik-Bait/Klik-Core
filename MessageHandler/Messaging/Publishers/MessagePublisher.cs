using MassTransit;
using MessageHandler.Messaging.Contracts;

namespace MessageHandler.Messaging.Publishers;

public class MessagePublisher(IPublishEndpoint publishEndpoint): IMessagePublisher
{
    public async Task PublishChatMessageAsync(string user, string message)
    {
        var chatMessage = new ChatMessage(user, message, DateTime.Now);
            
        await publishEndpoint.Publish(chatMessage);
    }
}