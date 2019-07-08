using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using GlobalX.ChatBots.WebexTeams.Mappers;
using GlobalX.ChatBots.WebexTeams.Models;
using GlobalXMessage = GlobalX.ChatBots.Core.Messages.Message;
using WebexTeamsMessage = GlobalX.ChatBots.WebexTeams.Models.Message;

namespace GlobalX.ChatBots.WebexTeams.Services
{
    public class WebexTeamsApiService : IWebexTeamsApiService
    {
        private readonly IHttpClientProxy _httpClientProxy;
        private readonly IMapper _mapper;

        public WebexTeamsApiService(IHttpClientProxy httpClientProxy, IWebexTeamsMapper mapper)
        {
            _httpClientProxy = httpClientProxy;
            _mapper = mapper;
        }

        public async Task<GlobalXMessage> GetMessageAsync(string messageId)
        {
            var result = await _httpClientProxy.GetAsync($"/messages/{messageId}").ConfigureAwait(false);
            var message = JsonConvert.DeserializeObject<WebexTeamsMessage>(result);
            // TODO create service to map properties which can't be handled by AutoMapper and use here. Same below
            var mapped = _mapper.Map<GlobalXMessage>(message);
            return mapped;
        }

        public async Task<GlobalXMessage> SendMessageAsync(GlobalXMessage message)
        {
            var body = _mapper.Map<CreateMessageRequest>(message);
            var json = JsonConvert.SerializeObject(body);
            var result = await _httpClientProxy.PostAsync("/messages", json).ConfigureAwait(false);
            var resultObject = JsonConvert.DeserializeObject<WebexTeamsMessage>(result);
            var mapped = _mapper.Map<GlobalXMessage>(resultObject);
            return mapped;
        }
    }
}
