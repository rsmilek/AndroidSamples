using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Adapter;
using MQTTnet.Channel;
using MQTTnet.Client;
using MQTTnet.Diagnostics;
using MQTTnet.Exceptions;
using MQTTnet.Implementations;
using MQTTnet.Internal;
using MQTTnet.Packets;
using MQTTnet.Protocol;
using MQTTnet.Serializer;
using MQTTnet.Server;
using MqttNetApp;

namespace MqttNetApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var timeMeas = new TimeMeas();
            var mqttClient = new MqttFactory().CreateMqttClient();


            // Create TCP based options using the builder.
            var options = new MqttClientOptionsBuilder()
                .WithClientId("Client1")
                .WithTcpServer("89.203.252.106")
                .WithCredentials("rsm", "pecnamspadla")
                //.WithTls()
                .WithCleanSession()
                .Build();


            mqttClient.ApplicationMessageReceived += (s, e) =>
            {
                Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
                Console.WriteLine($"+ Topic = {e.ApplicationMessage.Topic}");
                Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
                Console.WriteLine($"+ QoS = {e.ApplicationMessage.QualityOfServiceLevel}");
                Console.WriteLine($"+ Retain = {e.ApplicationMessage.Retain}");

                timeMeas.Stop();
                Console.WriteLine("{0} = Time from publish to receive", timeMeas.DiffSecondsStr);

                Console.WriteLine();
            };


            mqttClient.Connected += async (s, e) =>
            {
                Console.WriteLine("### CONNECTED WITH SERVER ###");

                await mqttClient.SubscribeAsync(new List<TopicFilter>
                {
                    //new TopicFilter("#", MqttQualityOfServiceLevel.AtMostOnce)

                    new TopicFilter("pokus/#", MqttQualityOfServiceLevel.ExactlyOnce)
                });

                Console.WriteLine("### SUBSCRIBED ###");
            };


            mqttClient.Disconnected += async (s, e) =>
            {
                Console.WriteLine("### DISCONNECTED FROM SERVER ###");
                await Task.Delay(TimeSpan.FromSeconds(5));                  

                try
                {
                    await mqttClient.ConnectAsync(options);
                }
                catch
                {
                    Console.WriteLine("### RECONNECTING FAILED ###");
                }
            };


            try
            {
                mqttClient.ConnectAsync(options);
            }
            catch
            {
                Console.WriteLine("### CONNECTING FAILED ###");
            }

            Console.WriteLine("### WAITING FOR APPLICATION MESSAGES ###");
            Console.WriteLine("### Press key 'q/Q' to terminate, other key publishes test message ###");


            while (true)
            {
                while (Console.ReadKey(true).Key == ConsoleKey.Q)
                    { Environment.Exit(0); };

                timeMeas.Start();

                var message = new MqttApplicationMessageBuilder()
                    .WithTopic("pokus/1")
                    .WithPayload("Hello from RSm")
                    .WithExactlyOnceQoS()
                    .Build();

                mqttClient.PublishAsync(message);
            }

        }
    }

}
