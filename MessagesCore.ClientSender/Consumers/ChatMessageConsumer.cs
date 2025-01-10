using MassTransit;
using MessageCore.Contracts;

namespace MessagesCore.ClientSender.Consumers;

public class ChatMessageConsumer(ILogger<ChatMessageConsumer> logger) : IConsumer<ChatMessage>
{
    public Task Consume(ConsumeContext<ChatMessage> context)
    {
        logger.LogDebug($"Received message: {context.Message.Message}");
        return Task.CompletedTask;
    }
}