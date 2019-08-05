using System.Threading.Tasks;
using GlobalX.ChatBots.Core.Messages;

namespace GlobalX.ChatBots.WebexTeams
{
    public interface IWebexTeamsWebhookHandler
    {
        Task RegisterWebhooksAsync();
        Task<Message> ProcessMessageWebhookCallbackAsync(string body);
    }
}
