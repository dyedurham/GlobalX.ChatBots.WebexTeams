namespace GlobalX.ChatBots.WebexTeams.Configuration
{
    public class WebexTeamsSettings
    {
        public string WebexTeamsApiUrl { get; set; }
        public string BotAuthToken { get; set; }
        public Webhook[] Webhooks { get; set; }
    }
}
