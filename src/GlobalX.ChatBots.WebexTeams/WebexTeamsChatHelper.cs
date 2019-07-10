using GlobalX.ChatBots.Core;
using GlobalX.ChatBots.Core.Messages;
using GlobalX.ChatBots.Core.People;
using GlobalX.ChatBots.Core.Rooms;

namespace GlobalX.ChatBots.WebexTeams
{
    internal class WebexTeamsChatHelper : IChatHelper
    {
        public WebexTeamsChatHelper(IMessageHandler messages, IPersonHandler people, IRoomHandler rooms)
        {
            Messages = messages;
            People = people;
            Rooms = rooms;
        }

        public IMessageHandler Messages { get; set; }
        public IPersonHandler People { get; set; }
        public IRoomHandler Rooms { get; set; }
    }
}
