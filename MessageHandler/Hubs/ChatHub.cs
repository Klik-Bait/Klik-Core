using MessageHandler.Messaging.Contracts;
using MessageHandler.Messaging.Publishers;
using Microsoft.AspNetCore.SignalR;

namespace MessageHandler.Hubs;

public class ChatHub(IMessagePublisher messagePublisher) : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await messagePublisher.PublishChatMessageAsync(user, message);
    }
}