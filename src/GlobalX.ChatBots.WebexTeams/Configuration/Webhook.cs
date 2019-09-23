namespace GlobalX.ChatBots.WebexTeams.Configuration
{
    public class Webhook
    {
        public string Name { get; set; }
        public string TargetUrl { get; set; }
        public ResourceType Resource { get; set; }
        public EventType Event { get; set; }
        public string Filter { get; set; }
    }
}
