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
}
