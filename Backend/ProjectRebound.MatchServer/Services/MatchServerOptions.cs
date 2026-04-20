namespace ProjectRebound.MatchServer.Services;

public sealed class MatchServerOptions
{
    public int HeartbeatSeconds { get; set; } = 5;
    public int StaleAfterSeconds { get; set; } = 15;
    public int HostLostAfterSeconds { get; set; } = 45;
    public int HostProbeSeconds { get; set; } = 60;
    public int JoinTicketSeconds { get; set; } = 90;
    public int MatchTicketSeconds { get; set; } = 120;
    public int EndedRoomRetentionMinutes { get; set; } = 30;
}
