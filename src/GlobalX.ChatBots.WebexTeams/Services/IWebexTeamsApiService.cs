using System.Threading.Tasks;
using GlobalX.ChatBots.Core.Messages;

namespace GlobalX.ChatBots.WebexTeams.Services
{
    public interface IWebexTeamsApiService
    {
        Task<Message> GetMessageAsync(string messageId);
        Task<Message> SendMessageAsync(Message message);
    }
}
