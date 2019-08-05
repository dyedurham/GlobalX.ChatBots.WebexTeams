using System.Threading.Tasks;
using GlobalX.ChatBots.WebexTeams.Configuration;
using GlobalX.ChatBots.WebexTeams.Mappers;
using GlobalX.ChatBots.WebexTeams.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Message = GlobalX.ChatBots.Core.Messages.Message;

namespace GlobalX.ChatBots.WebexTeams.Services
{
    internal class WebexTeamsWebhookHandler : IWebexTeamsWebhookHandler
    {
        private readonly IWebexTeamsApiService _apiService;
        private readonly WebexTeamsSettings _settings;
        private readonly IWebexTeamsMapper _mapper;
        private readonly IWebexTeamsMessageParser _messageParser;

        public WebexTeamsWebhookHandler(IWebexTeamsApiService apiService,
            IOptions<WebexTeamsSettings> settings, IWebexTeamsMapper mapper,
            IWebexTeamsMessageParser messageParser)
        {
            _apiService = apiService;
            _settings = settings.Value;
            _mapper = mapper;
            _messageParser = messageParser;
        }

        public async Task RegisterWebhooksAsync()
        {
            var hooks = await _apiService.GetWebhooksAsync();

            foreach (var hook in hooks)
            {
                await _apiService.DeleteWebhookAsync(hook.Id);
            }

            if (_settings.Webhooks == null)
            {
                return;
            }

            foreach (var newHook in _settings.Webhooks)
            {
                var mappedHook = _mapper.Map<CreateWebhookRequest>(newHook);
                await _apiService.CreateWebhookAsync(mappedHook);
            }
        }

        public async Task<Message> ProcessMessageWebhookCallbackAsync(string body)
        {
            var data = JsonConvert.DeserializeObject<MessageWebhookCallback>(body);
            var message = await _apiService.GetMessageAsync(data.Data.Id);
            return _messageParser.ParseMessage(message);
        }
    }
}
