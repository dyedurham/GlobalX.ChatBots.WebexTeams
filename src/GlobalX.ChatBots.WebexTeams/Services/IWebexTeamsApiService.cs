using System.Threading.Tasks;
using GlobalX.ChatBots.WebexTeams.Models;

namespace GlobalX.ChatBots.WebexTeams.Services
{
    internal interface IWebexTeamsApiService
    {
        Task<Message> GetMessageAsync(string messageId);
        Task<Message> SendMessageAsync(CreateMessageRequest request);
    }
}
