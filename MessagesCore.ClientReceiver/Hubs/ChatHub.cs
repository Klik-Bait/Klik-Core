using MessageCore.Contracts;
using MessagesCore.ClientReceiver.Messaging.Publishers;
using Microsoft.AspNetCore.SignalR;

namespace MessagesCore.ClientReceiver.Hubs;

public class ChatHub(IMessagePublisher messagePublisher) : Hub
{
    public async Task SendMessage(ChatMessage chatMessage)
    {
        var isMessageDeliveredToMessageBus = await messagePublisher.PublishChatMessageAsync(chatMessage);
        if (isMessageDeliveredToMessageBus)
        {
            await Clients.Caller.SendAsync("MessageAcknowledged", true);
        }
    }
}