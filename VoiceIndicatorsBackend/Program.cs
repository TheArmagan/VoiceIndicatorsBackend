using Microsoft.AspNet.SignalR;
using StackExchange.Redis;
using System.Text.Json;
using System.Text.Json.Serialization;
using VoiceIndicatorsBackend;

var builder = WebApplication.CreateBuilder(args);
ConfigureRedis(builder);
builder.Services.AddHttpClient();
//JsonSerializerOptions serializer = new()
//{
//    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
//    PropertyNameCaseInsensitive = true,
//    UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip,
//    Converters = { new JsonStringEnumConverter() }
    
//};
//GlobalHost.DependencyResolver.Register(typeof(JsonSerializer), () => serializer);
builder.Services.AddSignalR()
//    .AddJsonProtocol().AddHubOptions<IndicatorsHub>(x =>
//{
//    x.SupportedProtocols = new List<string> { "json" };
//    x.EnableDetailedErrors = true;
//})
;
var app = builder.Build();
app.MapHub<IndicatorsHub>("/indicators");
app.Run();

static void ConfigureRedis(WebApplicationBuilder builder)
{
    string redisConnectionString = builder.Configuration.GetValue<string>("RedisConnection")!;
    var redisConnection = ConnectionMultiplexer.Connect(redisConnectionString);
    builder.Services.AddSingleton<IConnectionMultiplexer>(redisConnection);
    builder.Services.AddScoped<IDatabase>(ctx => ctx.GetRequiredService<IConnectionMultiplexer>().GetDatabase());
}