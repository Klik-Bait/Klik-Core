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

connection.On<bool>("MessageAcknowledged", (success) =>
{
    if(success)
        Console.WriteLine($"Message was delivered");
});

// Loop to send messages
Console.WriteLine("Enter your name:");
var userName = Console.ReadLine();

while (true)
{
    Console.WriteLine("Type a message (or type 'exit' to quit):");
    var message = Console.ReadLine();

    if (message?.ToLower() == "exit")
        break;

    try
    {
        var chatMessage = new ChatMessage(userName ?? "adam", "Adrian" ,message ?? "Typical message" , DateTime.Now);
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