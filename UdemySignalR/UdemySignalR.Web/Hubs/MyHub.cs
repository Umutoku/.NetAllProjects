using Microsoft.AspNetCore.SignalR;

namespace UdemySignalR.Web.Hubs
{
    public class MyHub:Hub
    {
        public async Task SendMessage(String message)
        {
            await Clients.All.SendAsync(message);
        }
    }
}
