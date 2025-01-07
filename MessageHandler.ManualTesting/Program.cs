using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5193/ChatHub")
    .Build();

connection.On<string, string>("ReceiveMessage", (user, message) =>
{
    Console.WriteLine($"{user}: {message}");
});

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
        // Send the message to the SignalR hub
        await connection.InvokeAsync("SendMessage", userName, message);
        Console.WriteLine("Message sent.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error sending message: {ex.Message}");
    }
}

// Stop the connection before exiting
await connection.StopAsync();
Console.WriteLine("Connection stopped.");