using System.Threading.Tasks;
using Newtonsoft.Json;
using GlobalXMessage = GlobalX.ChatBots.Core.Messages.Message;
using WebexTeamsMessage = GlobalX.ChatBots.WebexTeams.Models.Message;

namespace GlobalX.ChatBots.WebexTeams.Services
{
    internal class WebexTeamsApiService : IWebexTeamsApiService
    {
        private readonly IHttpClientProxy _httpClientProxy;
        private readonly IWebexTeamsMessageParser _messageParser;

        public WebexTeamsApiService(IHttpClientProxy httpClientProxy, IWebexTeamsMessageParser messageParser)
        {
            _httpClientProxy = httpClientProxy;
            _messageParser = messageParser;
        }

        public async Task<GlobalXMessage> GetMessageAsync(string messageId)
        {
            var result = await _httpClientProxy.GetAsync($"/messages/{messageId}").ConfigureAwait(false);
            var message = JsonConvert.DeserializeObject<WebexTeamsMessage>(result);
            var mapped = _messageParser.ParseMessage(message);
            return mapped;
        }

        public async Task<GlobalXMessage> SendMessageAsync(GlobalXMessage message)
        {
            var body = _messageParser.ParseCreateMessageRequest(message);
            var json = JsonConvert.SerializeObject(body);
            var result = await _httpClientProxy.PostAsync("/messages", json).ConfigureAwait(false);
            var resultObject = JsonConvert.DeserializeObject<WebexTeamsMessage>(result);
            var mapped = _messageParser.ParseMessage(resultObject);
            return mapped;
        }
    }
}
