using System.Threading.Tasks;
using GlobalX.ChatBots.WebexTeams.Configuration;
using GlobalX.ChatBots.WebexTeams.Mappers;
using GlobalX.ChatBots.WebexTeams.Models;
using Microsoft.Extensions.Options;

namespace GlobalX.ChatBots.WebexTeams.Services
{
    internal class WebhookService : IWebhookService
    {
        private readonly IWebexTeamsApiService _apiService;
        private readonly WebexTeamsSettings _settings;
        private readonly IWebexTeamsMapper _mapper;

        public WebhookService(IWebexTeamsApiService apiService, IOptions<WebexTeamsSettings> settings, IWebexTeamsMapper mapper)
        {
            _apiService = apiService;
            _settings = settings.Value;
            _mapper = mapper;
        }

        public async Task RegisterWebhooks()
        {
            var hooks = await _apiService.GetWebhooksAsync();

            foreach (var hook in hooks)
            {
                await _apiService.DeleteWebhookAsync(hook.Id);
            }

            foreach (var newHook in _settings.Webhooks)
            {
                var mappedHook = _mapper.Map<CreateWebhookRequest>(newHook);
                await _apiService.CreateWebhookAsync(mappedHook);
            }
        }
    }
}
