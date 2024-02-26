// See https://aka.ms/new-console-template for more information
using AzureStorageLibrary.Services;
using System.Text;

Console.WriteLine("Hello, World!");

AzureStorageLibrary.ConnectionStrings.AzureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=az204storagedemo;AccountKey=3Z";

AzureQueue queue = new AzureQueue("az204queue");

string base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes("Hello, World!"));

await queue.SendMessageAsync(base64, default);

Console.WriteLine("Message sent!");

string message = await queue.ReceiveMessageAsync();

string messageDecoded = Encoding.UTF8.GetString(Convert.FromBase64String(message));

Console.WriteLine(messageDecoded);

Console.WriteLine(message == base64 ? "Message received!" : "Message not received!");

Console.ReadLine();

