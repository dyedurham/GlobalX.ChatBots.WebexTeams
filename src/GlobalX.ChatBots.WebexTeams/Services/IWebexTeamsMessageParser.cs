using GlobalX.ChatBots.WebexTeams.Models;
using GlobalXMessage = GlobalX.ChatBots.Core.Messages.Message;
using WebexTeamsMessage = GlobalX.ChatBots.WebexTeams.Models.Message;

namespace GlobalX.ChatBots.WebexTeams.Services
{
    public interface IWebexTeamsMessageParser
    {
        GlobalXMessage ParseMessage(WebexTeamsMessage message);
        CreateMessageRequest ParseCreateMessageRequest(GlobalXMessage message);
    }
}
