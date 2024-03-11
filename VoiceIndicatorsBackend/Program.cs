using Microsoft.AspNet.SignalR;
using StackExchange.Redis;
using System.Text.Json;
using System.Text.Json.Serialization;
using VoiceIndicatorsBackend;

var builder = WebApplication.CreateBuilder(args);
ConfigureRedis(builder);
builder.Services.AddHttpClient();
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowDiscord", builder =>
    {
        builder.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins("https://discord.com");
    });
});
var app = builder.Build();
app.UseCors("AllowDiscord");
app.MapHub<VoiceIndicatorsHub>("/voice-indicators");
app.Run();

static void ConfigureRedis(WebApplicationBuilder builder)
{
    string redisConnectionString = builder.Configuration.GetValue<string>("RedisConnection")!;
    var redisConnection = ConnectionMultiplexer.Connect(redisConnectionString);
    builder.Services.AddSingleton<IConnectionMultiplexer>(redisConnection);
    builder.Services.AddScoped<IDatabase>(ctx => ctx.GetRequiredService<IConnectionMultiplexer>().GetDatabase());
}