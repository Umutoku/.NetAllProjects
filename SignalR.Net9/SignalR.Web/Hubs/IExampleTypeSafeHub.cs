using SignalR.Web.Models;
using System.Data.Common;

namespace SignalR.Web.Hubs
{
    public interface IExampleTypeSafeHub
    {
        Task ReceiveMessageForAllClient(string message);
        Task ReceiveMessageAsStreamForAllClient(string name);
        Task ReceiveProductAsStreamForAllClient(Product product);
        Task ReceiveTypeMessageForAllClient(Product product);
        Task ReceiveMessageForCallerClient(string message);
        Task ReceiveMessageForOtherClient(string message);
        Task ReceiveMessageForIndividualClient(string message);
        Task ReceiveMessageForGroupClient(string message);
        Task ReceiveConnectedClientCountAllClient(int clientCount);

    }
}
