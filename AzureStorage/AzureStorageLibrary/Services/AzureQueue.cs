using Azure.Storage.Queues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorageLibrary.Services
{
    public class AzureQueue
    {
        private readonly QueueServiceClient _queueServiceClient; 
        private readonly QueueClient _queueClient;

        public AzureQueue(string queueName)
        {
            _queueServiceClient = new QueueServiceClient(ConnectionStrings.AzureStorageConnectionString);
            _queueClient = _queueServiceClient.GetQueueClient(queueName);
            _queueClient.CreateIfNotExists(); // eğer queue yoksa oluşturur
        }

        public async Task SendMessageAsync(string message, CancellationToken cancellation)
        {
            await _queueClient.SendMessageAsync(message,default,default,cancellation); // mesajı gönderir ve gönderme işlemi iptal edilebilir
        }

        public async Task<string> ReceiveMessageAsync()
        {
            var messages = await _queueClient.ReceiveMessagesAsync(default,TimeSpan.FromMinutes(1));  // mesajı alır ve mesaj alımı 1 dakika içinde gerçekleşmezse iptal edilir

            // _queueClient.PeekMessageAsync(); // mesajı alır ve silmez

            var message = messages.Value.FirstOrDefault(); // alınan mesajın ilkini seçer
            return message.MessageText; // mesajın içeriğini döner
        }

        public async Task DeleteMessageAsync(string messageId, string popReceipt) // mesajı siler, silinmesi için mesajın id'si ve popreceipt'i gereklidir
        {
            await _queueClient.DeleteMessageAsync(messageId, popReceipt);
        }
    }
}
