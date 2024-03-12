using Microsoft.AspNetCore.SignalR;
using StackExchange.Redis;
using System.Collections.Concurrent;
using System.Text.Json;
namespace VoiceIndicatorsBackend;

public class VoiceIndicatorsHub : Hub
{
    IDatabase _database;

    public VoiceIndicatorsHub(IDatabase database)
    {
        _database = database;
        try
        {
            _database.ExecuteAsync("FT.CREATE", "VIChannels ON JSON PREFIX 1 VI:Channels: SCHEMA $.UserIds.* AS UserIds TAG $.SenderUserIds.* AS SenderUserIds TAG $.GuildId AS GuildId TAG");
        } catch { }
    }
    ConcurrentDictionary<string, string> _connections = new ConcurrentDictionary<string, string>();
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }

    //public void Test(string props)
    //{
    //    var test = JsonSerializer.Deserialize<Test>(props);
    //    Console.Out.WriteLine();
    //}

    public void VoiceStateUpdate(string eventString)
    {
        var voiceStateUpdate = JsonSerializer.Deserialize<VoiceStateUpdateType>(eventString);
    }

   public void Login(string username)
    {
        _connections.AddOrUpdate(Context.ConnectionId, username, (key, value) => value);
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
        try
        {
            _connections.TryRemove(_connections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key, out string? _);
        } catch { }
        Console.Out.WriteLine("Disconnected: "+Context.ConnectionId+", Size: "+_connections.Count);
        await base.OnDisconnectedAsync(exception);
    }

    private void OnVoiceJoin(BackendState state)
    {
        var SenderId = _connections.First(x => x.Key == Context.ConnectionId)!.Value;
    }
}
