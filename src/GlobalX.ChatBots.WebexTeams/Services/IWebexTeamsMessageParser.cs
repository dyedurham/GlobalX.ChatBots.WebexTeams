using GlobalX.ChatBots.WebexTeams.Models;
using Message = GlobalX.ChatBots.Core.Messages.Message;

namespace GlobalX.ChatBots.WebexTeams.Services
{
    internal interface IWebexTeamsMessageParser
    {
        Message ParseMessage(Models.Message message);
        CreateMessageRequest ParseCreateMessageRequest(Message message);
    }
}