// See https://aka.ms/new-console-template for more information
// create signalr client
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using VoiceIndicatorClient;

var userName = Console.ReadLine();
var connection = new HubConnectionBuilder()
    .ConfigureLogging(logging => { logging.SetMinimumLevel(LogLevel.Debug); logging.AddConsole(); })
    .WithUrl("http://localhost:5232/voice-indicators")
    .WithAutomaticReconnect()
    .Build();
connection.On<string>("ReceiveMessage", (message) =>
{
    Console.WriteLine(message);
});
await connection.StartAsync();

await connection.SendAsync("Login", userName);

await connection.SendAsync("Test", JsonSerializer.Serialize(new Test
{
    Test1 = 1,
    Test2 = "test",
    Test3 = new int[] { 1, 2, 3 },
    Test4 = new Test[] {
        new Test {
            Test1 = 1,
            Test2 = "test",
            Test4 = new Test[]
            {
                new Test
                {
                    Test1 = 1,
                    Test2 = "test",
                    Test3 = new int[] { 1, 2, 3 }
                }
            }
        }
    }
}));

Console.WriteLine("Connected to hub");
Console.ReadLine();