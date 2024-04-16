﻿using Microsoft.AspNetCore.SignalR;

namespace SignalR.Web.Hubs
{
    public class ExampleHub : Hub
    {
        public async Task BroadcastMessageAllClientCall(string message)
        {
            await Clients.All.SendAsync("BroadcastMessageAllClient", message);
        }
    }
}
