using MessageCore.Contracts;
using MessagesCore.ClientReceiver.Messaging.Publishers;
using Microsoft.AspNetCore.SignalR;
using StackExchange.Redis;

namespace MessagesCore.ClientReceiver.Hubs;

public class ChatHub(IMessagePublisher messagePublisher, IConnectionMultiplexer redisConnection) : Hub
{
    private IDatabase Database => redisConnection.GetDatabase();

    public async Task SendMessage(ChatMessage chatMessage)
    {   
        if(await IsUserOnline(chatMessage.Receiver.Id))
        {
            var response = await messagePublisher.PublishChatMessageAsync(chatMessage);
            if (response.IsDelivered)
            {
                await Clients.Caller.SendAsync("MessageAcknowledged", response);
            }
        }

    }

    private async Task<bool> IsUserOnline(Guid userId)
    {
        // Build the Redis key for this user
        string key = $"UserConnections:{userId}";

        // Check how many connection IDs are stored
        var connectionCount = await Database.SetLengthAsync(key);

        return connectionCount > 0;
    }
}