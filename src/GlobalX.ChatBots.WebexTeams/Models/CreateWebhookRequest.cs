namespace GlobalX.ChatBots.WebexTeams.Models
{
    internal class CreateWebhookRequest
    {
        public string Name { get; set; }
        public string TargetUrl { get; set; }
        public string Resource { get; set; }
        public string Event { get; set; }
        public string Filter { get; set; }
        public string Secret { get; set; }
    }
}
