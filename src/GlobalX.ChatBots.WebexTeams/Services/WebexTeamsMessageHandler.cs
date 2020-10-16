using System.Threading.Tasks;
using GlobalX.ChatBots.Core.Messages;
using GlobalX.ChatBots.WebexTeams.Mappers;
using GlobalX.ChatBots.WebexTeams.Models;
using Message = GlobalX.ChatBots.Core.Messages.Message;
using Person = GlobalX.ChatBots.Core.People.Person;

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

            Models.Message result;
            try
            {
                result = await _apiService.SendMessageAsync(request).ConfigureAwait(false);
            }
            catch (InvalidParentException)
            {
                result = await ResendMessageWithRootParentId(message);
            }

            return await GetSentMessageWithSender(result);
        }

        private async Task<Models.Message> ResendMessageWithRootParentId(Message message)
        {
            var realParent = await _apiService.GetMessageAsync(message.ParentId);
            message.ParentId = realParent.Id;

            var request = _messageParser.ParseCreateMessageRequest(message);
            var result = await _apiService.SendMessageAsync(request).ConfigureAwait(false);
            return result;
        }

        private async Task<Message> GetSentMessageWithSender(Models.Message result)
        {
            var mapped = _messageParser.ParseMessage(result);
            var sender = await _apiService.GetPersonAsync(result.PersonId);
            var mappedSender = _mapper.Map<Person>(sender);
            mapped.Sender = mappedSender;
            return mapped;
        }
    }
}
