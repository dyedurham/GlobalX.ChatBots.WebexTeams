using System.Threading.Tasks;
using Newtonsoft.Json;
using GlobalX.ChatBots.WebexTeams.Models;

namespace GlobalX.ChatBots.WebexTeams.Services
{
    public class WebexTeamsApiService : IWebexTeamsApiService
    {
        private readonly IHttpClientProxy _httpClientProxy;

        public WebexTeamsApiService(IHttpClientProxy httpClientProxy)
        {
            _httpClientProxy = httpClientProxy;
        }

        public async Task<Message> GetMessageAsync(string messageId)
        {
            var result = await _httpClientProxy.GetAsync($"/messages/{messageId}").ConfigureAwait(false);
            var message = JsonConvert.DeserializeObject<Message>(result);
            return message;
        }

        public async Task<Message> SendMessageAsync(CreateMessageRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var result = await _httpClientProxy.PostAsync("/messages", json).ConfigureAwait(false);
            var resultObject = JsonConvert.DeserializeObject<Message>(result);
            return resultObject;
        }
    }
}
