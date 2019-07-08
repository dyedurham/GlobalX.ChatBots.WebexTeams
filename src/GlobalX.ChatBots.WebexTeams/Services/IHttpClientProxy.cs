using System.Threading.Tasks;

namespace GlobalX.ChatBots.WebexTeams.Services
{
    public interface IHttpClientProxy
    {
        Task<string> GetAsync(string path, string body = null);
        Task<string> PostAsync(string path, string body = null);
    }
}
