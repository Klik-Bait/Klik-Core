namespace MessageCore.Contracts;

public class ChatDeliveryResponse
{
    public ChatDeliveryResponse(User receiver)
    {
        Receiver = receiver;
    }

    public bool IsDelivered { get; set; } = false;
    public User Receiver { get; init; }
}

