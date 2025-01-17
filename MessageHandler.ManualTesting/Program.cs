using MessageCore.Contracts;
using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5193/ChatHub")
    .Build();

try
{
    await connection.StartAsync();
    Console.WriteLine("Connected to the SignalR server.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error connecting to the server: {ex.Message}");
    return;
}

connection.On<ChatDeliveryResponse>("MessageAcknowledged", (chatDelivery) =>
{
    if(chatDelivery.IsDelivered)
        Console.WriteLine($"Message was delivered to {chatDelivery.Receiver.Name}");
});

// Loop to send messages
Console.WriteLine("Enter your name: (default: Krystian)");
var senderConsoleResult = Console.ReadLine();
var senderUserName = string.IsNullOrWhiteSpace(senderConsoleResult) ? "Krystian" : senderConsoleResult.Trim();

Console.WriteLine("Enter receiver name: (default: Adrian)");
var receiverConsoleResult = Console.ReadLine();
var receiverUserName = string.IsNullOrWhiteSpace(receiverConsoleResult) ? "Adrian" : receiverConsoleResult.Trim();

while (true)
{
    Console.WriteLine("Type a message (or type 'exit' to quit):");
    var message = Console.ReadLine();

    if (message?.ToLower() == "exit")
        break;

    try
    {
        var chatMessage = new ChatMessage(new User(senderUserName) , new User(receiverUserName), message!, DateTime.Now);
        await connection.InvokeAsync("SendMessage", chatMessage);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error sending message: {ex.Message}");
    }
}

// Stop the connection before exiting
await connection.StopAsync();
Console.WriteLine("Connection stopped.");