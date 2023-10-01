using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Server;
using System.Runtime.CompilerServices;

namespace mqttdeneme
{
    class Publisher
    {
        static async Task Main(string[] args)
        {
            var mqttFactory = new MqttFactory();
            IMqttClient _mqttClient = mqttFactory.CreateMqttClient();
            var options = new MqttClientOptionsBuilder().
                WithClientId("mqttx_35472e4c")
                .WithTcpServer("broker.emqx.io", 1883)
                
                .WithCleanSession()
            .Build();
            
            
            await _mqttClient.ConnectAsync(options, CancellationToken.None);

            Console.WriteLine("Lütfen tuşa bas gönder bebek");

            Console.ReadLine();

            PublishMessageAsync(_mqttClient);
            
        }

        private static async Task PublishMessageAsync(IMqttClient mqttClient)
        {
            string messagePayload = "Core ekibi uçabiliyor";
            var message = new MqttApplicationMessageBuilder()
                .WithTopic("testtopiccore")
                .WithPayload(messagePayload)
                .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtMostOnce)
                .Build();
            if (mqttClient.IsConnected)
            {
                await mqttClient.PublishAsync(message);
            }
        }
    }
}

//_mqttClient.UseApplicationMessageReceivedHandler(e =>
//{
//    Console.WriteLine("Received application message.");
//    e.DumpToConsole();
//    return Task.CompletedTask;
//});