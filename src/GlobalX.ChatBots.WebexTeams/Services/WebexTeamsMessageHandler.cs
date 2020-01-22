using System.Threading.Tasks;
using GlobalX.ChatBots.Core.Messages;
using GlobalX.ChatBots.Core.People;
using GlobalX.ChatBots.WebexTeams.Mappers;

namespace GlobalX.ChatBots.WebexTeams.Services
{
    internal class WebexTeamsMessageHandler : IMessageHandler
    {
        private readonly IWebexTeamsApiService _apiService;
        private readonly IWebexTeamsMessageParser _messageParser;
        private readonly IWebexTeamsMapper _mapper;

        public WebexTeamsMessageHandler(IWebexTeamsApiService apiService, IWebexTeamsMessageParser messageParser,
            IWebexTeamsMapper mapper)
        {
            _apiService = apiService;
            _messageParser = messageParser;
            _mapper = mapper;
        }

        public async Task<Message> SendMessageAsync(Message message)
        {
            var request = _messageParser.ParseCreateMessageRequest(message);
            var result = await _apiService.SendMessageAsync(request).ConfigureAwait(false);
            var mapped = _messageParser.ParseMessage(result);
            var sender = await _apiService.GetPersonAsync(result.PersonId);
            var mappedSender = _mapper.Map<Person>(sender);
            mapped.Sender = mappedSender;
            return mapped;
        }
    }
}
