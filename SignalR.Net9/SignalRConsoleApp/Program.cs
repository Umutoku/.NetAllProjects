using Microsoft.AspNetCore.SignalR.Client;
using SignalRConsoleApp;

Console.WriteLine("SignalR Console Client");

var connection = new HubConnectionBuilder()
    .WithUrl("https://localhost:5001/exampletypesafehub")
    .WithAutomaticReconnect()
    .Build();

connection.StartAsync().ContinueWith(result =>
{
    if (result.IsFaulted)
    {
        Console.WriteLine("Connection failed");
        return;
    }
    Console.WriteLine("Connection started");

});

connection.On<Product>("ReceiveTypedMessageForAllClient", (product) =>
{
    Console.WriteLine($"ReceiveMessageForAllClient: {product.Id}, {product.Name}");
});

while (true)
{
    Console.WriteLine("Enter a message (or 'exit' to exit):");
    var message = Console.ReadLine();

    if (message == "exit")
    {
        break;
    }

    var product = new Product(1, "Product 1", 100);
    await connection.InvokeAsync("BroadcastTypedMessageToAllClient", product);
}