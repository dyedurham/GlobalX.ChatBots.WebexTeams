using GlobalX.ChatBots.Core;
using GlobalX.ChatBots.Core.Messages;
using GlobalX.ChatBots.Core.People;
using GlobalX.ChatBots.Core.Rooms;
using GlobalX.ChatBots.WebexTeams.Services;

namespace GlobalX.ChatBots.WebexTeams
{
    public class WebexTeamsChatHelper : IChatHelper, IWebhookHelper
    {
        private readonly IWebhookService _webhooks;

        public WebexTeamsChatHelper(IMessageHandler messages, IPersonHandler people, IRoomHandler rooms, IWebhookService webhooks)
        {
            _webhooks = webhooks;
            Messages = messages;
            People = people;
            Rooms = rooms;
        }

        public IMessageHandler Messages { get; set; }
        public IPersonHandler People { get; set; }
        public IRoomHandler Rooms { get; set; }

        public void RegisterWebhooks()
        {
            _webhooks.RegisterWebhooks();
        }
    }
}
