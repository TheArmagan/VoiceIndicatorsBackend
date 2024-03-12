namespace VoiceIndicatorsBackend
{
    public enum StateType
    {
        GuildDeaf,
        Deaf,
        GuildMute,
        Mute,
        Video,
        Stream,
        Normal
    }

    public enum StateUpdateType
    {
        Join,
        Leave,
        Move,
        Update
    }

    public class VoiceStateUpdateType
    {
        public BackendState? OldState { get; set; }
        public BackendState? NewState { get; set; }
        public StateUpdateType? Type { get; set; }
    }

    public class BackendState
    {
        public StateType? State { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserGlobalName { get; set; }
        public string? UserAvatar { get; set; }
        public string? ChannelId { get; set; }
        public string? ChannelName { get; set; }
        public string? ChannelIcon { get; set; }
        public string? GuildId { get; set; }
        public string? GuildName { get; set; }
        public string? GuildIcon { get; set; }
        public string? GuildVanityURLCode { get; set; }
    }

    public class RedisStateChannelUser
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserGlobalName { get; set; }
        public string? UserAvatar { get; set; }
        public StateType? State { get; set; }
        public long? JoinedAt { get; set; }
    }

    public class RedisStateChannel
    {
        public static string[]? UserIds { get; set; }
        public static string[]? JoinedBeforeUserIds { get; set; }
        public static long? LastValidatedAt { get; set; }


        public static RedisStateChannelUser[]? Users { get; set; }

        public static string[]? SenderUserIds { get; set; }


        public string? ChannelId { get; set; }
        public string? ChannelName { get; set; }
        public string? ChannelIcon { get; set; }
        public string? GuildId { get; set; }
        public string? GuildName { get; set; }
        public string? GuildIcon { get; set; }
        public string? GuildVanityURLCode { get; set; }
        public long? At { get; set; }
    }
}
