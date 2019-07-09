using System.Threading.Tasks;
using GlobalX.ChatBots.WebexTeams.Models;

namespace GlobalX.ChatBots.WebexTeams.Services
{
    public interface IWebexTeamsApiService
    {
        Task<Message> GetMessageAsync(string messageId);
        Task<Message> SendMessageAsync(CreateMessageRequest request);
    }
}
