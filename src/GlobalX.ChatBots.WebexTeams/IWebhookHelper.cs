using GlobalX.ChatBots.Core.Messages;

namespace GlobalX.ChatBots.WebexTeams
{
    public interface IWebhookHelper
    {
        IWebexTeamsWebhookHandler Webhooks { get; set; }
    }
}
