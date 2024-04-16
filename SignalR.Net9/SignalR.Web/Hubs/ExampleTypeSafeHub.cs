using Microsoft.AspNetCore.SignalR;
using SignalR.Web.Models;

namespace SignalR.Web.Hubs
{
    public class ExampleTypeSafeHub : Hub<IExampleTypeSafeHub>
    {
        private static int _connectionCount = 0;
        // Tüm clientlara mesaj gönderme
        public async Task BroadcastMessageAllClient(string message)
        {
            await Clients.All.ReceiveMessageForAllClient(message); // This is a strongly typed method call
        }
        // Çağıran clienta mesaj gönderme
        public async Task BroadcastMessageToCallerClient(string message)
        {
            await Clients.Caller.ReceiveMessageForCallerClient(message); // This is a strongly typed method call
        }
        // Kendi dışındaki diğer clientlara mesaj gönderme
        public async Task BroadcastMessageToOtherClient(string message)
        {
            await Clients.Others.ReceiveMessageForOtherClient(message); // This is a strongly typed method call
        }

        // Belirli bir clienta mesaj gönderme
        public async Task BroadcastMessageToIndividualClient(string connectionId, string message)
        {
            await Clients.Client(connectionId).ReceiveMessageForIndividualClient(message); // This is a strongly typed method call
        }
        // Belirli bir gruptaki clientlara mesaj gönderme
        public async Task BroadcastMessageToGroupClients(string groupName, string message)
        {
            await Clients.Group(groupName).ReceiveMessageForGroupClient(message); // This is a strongly typed method call
        }

        public async Task BroadcastMessageAsStreamToAllClient(IAsyncEnumerable<string> nameAsChunks)
        {
            await foreach (var name in nameAsChunks)
            {
                await Clients.All.ReceiveMessageAsStreamForAllClient(name); // This is a strongly typed method call
            }

        }

        public async Task BroadcastStreamProductToAllClient (IAsyncEnumerable<Product> products)
        {
            await foreach (var product in products)
            {
                await Clients.All.ReceiveProductAsStreamForAllClient(product); // This is a strongly typed method call
            }

        }

        public async IAsyncEnumerable<string> BroadcastCastFromToHubClient(int count)
        { 
            foreach (var i in Enumerable.Range(0, count).ToList())
            {
                yield return i.ToString();
            }
        }

        // Gruba katılma
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Caller.ReceiveMessageForCallerClient($"You have joined the group: {groupName}");
            await Clients.Others.ReceiveMessageForOtherClient($"New client joined the group: {groupName}");
            await Clients.Group(groupName).ReceiveMessageForGroupClient($"New client joined the group: {groupName}");
        }

        // Gruptan ayrılma
        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Caller.ReceiveMessageForCallerClient($"You have left the group: {groupName}");
            await Clients.Others.ReceiveMessageForOtherClient($"Client left the group: {groupName}");
            await Clients.Group(groupName).ReceiveMessageForGroupClient($"Client left the group: {groupName}");
        }

        public async Task BroadcastTypedMessageToAllClient(Product product)
        {
            await Clients.All.ReceiveTypeMessageForAllClient(product); // This is a strongly typed method call
        }

        // Bağlı client sayısını tüm clientlara gönderme
        public override async Task OnConnectedAsync()
        {
            _connectionCount++;
            await Clients.All.ReceiveConnectedClientCountAllClient(_connectionCount);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _connectionCount--;
            await Clients.All.ReceiveConnectedClientCountAllClient(_connectionCount);
            await base.OnDisconnectedAsync(exception);
        }

        
    }
}
