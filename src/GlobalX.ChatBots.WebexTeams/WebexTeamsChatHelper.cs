using GlobalX.ChatBots.Core;
using GlobalX.ChatBots.Core.Messages;
using GlobalX.ChatBots.Core.People;
using GlobalX.ChatBots.Core.Rooms;

namespace GlobalX.ChatBots.WebexTeams
{
    public class WebexTeamsChatHelper : IChatHelper, IWebhookHelper
    {
        public WebexTeamsChatHelper(IMessageHandler messages, IPersonHandler people, IRoomHandler rooms,
            IWebexTeamsWebhookHandler webhooks)
        {
            Messages = messages;
            People = people;
            Rooms = rooms;
            Webhooks = webhooks;
        }

        public IMessageHandler Messages { get; set; }
        public IPersonHandler People { get; set; }
        public IRoomHandler Rooms { get; set; }
        public IWebexTeamsWebhookHandler Webhooks { get; set; }
    }
}
