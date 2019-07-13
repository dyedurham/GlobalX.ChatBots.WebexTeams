using System.Threading.Tasks;
using GlobalX.ChatBots.Core.Messages;

namespace GlobalX.ChatBots.WebexTeams.Services
{
    internal class WebexTeamsMessageHandler : IMessageHandler
    {
        private readonly IWebexTeamsApiService _apiService;
        private readonly IWebexTeamsMessageParser _messageParser;

        public WebexTeamsMessageHandler(IWebexTeamsApiService apiService, IWebexTeamsMessageParser messageParser)
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
