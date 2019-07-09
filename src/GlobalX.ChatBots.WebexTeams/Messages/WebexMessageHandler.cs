using System.Threading.Tasks;
using GlobalX.ChatBots.Core.Messages;
using GlobalX.ChatBots.WebexTeams.Services;

namespace GlobalX.ChatBots.WebexTeams.Messages
{
    public class WebexMessageHandler : IMessageHandler
    {
        private readonly IWebexTeamsApiService _apiService;
        private readonly IWebexTeamsMessageParser _messageParser;

        public WebexMessageHandler(IWebexTeamsApiService apiService, IWebexTeamsMessageParser messageParser)
        {
            _apiService = apiService;
            _messageParser = messageParser;
        }

        public async Task<Message> SendMessageAsync(Message message)
        {
            var request = _messageParser.ParseCreateMessageRequest(message);
            var result = await _apiService.SendMessageAsync(request).ConfigureAwait(false);
            var mapped = _messageParser.ParseMessage(result);
            return mapped;
        }
    }
}
