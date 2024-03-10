using Microsoft.AspNetCore.SignalR;
using StackExchange.Redis;
using System.Collections.Concurrent;
using System.Text.Json;
namespace VoiceIndicatorsBackend;

public class IndicatorsHub : Hub
{
    IDatabase _database;

    public IndicatorsHub(IDatabase database)
    {
        _database = database;
    }
    ConcurrentDictionary<string, string> _connections = new ConcurrentDictionary<string, string>();
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }

    public void Test(string props)
    {
        var test = JsonSerializer.Deserialize<Test>(props);
        Console.Out.WriteLine();
    }


   public void Login(string username)
    {
        _connections.AddOrUpdate(username, Context.ConnectionId, (key, value) => value);
        Clients.All.SendAsync("ReceiveMessage", $"{username} joined the chat");
    }

    public void SendToUser(string to, string eventName, string content)
    {
        if (_connections.TryGetValue(to, out string? connectionId))
        {
            Clients.Client(connectionId).SendAsync(eventName, content);
        }
    }
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _connections.TryRemove(_connections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key, out string? _);
        await base.OnDisconnectedAsync(exception);
    }

}
