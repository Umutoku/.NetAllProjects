using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Server;
using System.Runtime.CompilerServices;

namespace mqttdeneme
{
    class Subscriber
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

            var topicfilter = new MqttTopicFilterBuilder()
                .WithTopic("testtopiccore")
                .Build();
            await _mqttClient.SubscribeAsync(topicfilter);

            await _mqttClient.ApplicationMessageReceivedAsync += (e => { Console.WriteLine("s"); });

            await _mqttClient.ConnectAsync(options, CancellationToken.None);
        }
    }
}