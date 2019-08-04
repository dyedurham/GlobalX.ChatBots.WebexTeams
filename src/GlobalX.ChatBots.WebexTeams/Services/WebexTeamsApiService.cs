using System.Threading.Tasks;
using GlobalX.ChatBots.WebexTeams.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GlobalX.ChatBots.WebexTeams.Services
{
    internal class WebexTeamsApiService : IWebexTeamsApiService
    {
        private readonly IHttpClientProxy _httpClientProxy;
        private readonly JsonSerializerSettings _defaultJsonSerializerSettings;

        public WebexTeamsApiService(IHttpClientProxy httpClientProxy)
        {
            _httpClientProxy = httpClientProxy;
            _defaultJsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public async Task<Message> GetMessageAsync(string messageId)
        {
            var result = await _httpClientProxy.GetAsync($"/messages/{messageId}").ConfigureAwait(false);
            var message = JsonConvert.DeserializeObject<Message>(result, _defaultJsonSerializerSettings);
            return message;
        }

        public async Task<Message> SendMessageAsync(CreateMessageRequest request)
        {
            var json = JsonConvert.SerializeObject(request, _defaultJsonSerializerSettings);
            var result = await _httpClientProxy.PostAsync("/messages", json).ConfigureAwait(false);
            var resultObject = JsonConvert.DeserializeObject<Message>(result, _defaultJsonSerializerSettings);
            return resultObject;
        }

        public async Task<Person> GetPersonAsync(string personId)
        {
            var result = await _httpClientProxy.GetAsync($"/people/{personId}").ConfigureAwait(false);
            var person = JsonConvert.DeserializeObject<Person>(result, _defaultJsonSerializerSettings);
            return person;
        }

        public async Task<Room> GetRoomAsync(string roomId)
        {
            var result = await _httpClientProxy.GetAsync($"/rooms/{roomId}").ConfigureAwait(false);
            var room = JsonConvert.DeserializeObject<Room>(result, _defaultJsonSerializerSettings);
            return room;
        }

        public async Task<Webhook[]> GetWebhooksAsync()
        {
            var result = await _httpClientProxy.GetAsync("/webhooks").ConfigureAwait(false);
            var response = JsonConvert.DeserializeObject<WebhookListResponse>(result, _defaultJsonSerializerSettings);
            return response.Items;
        }

        public async Task<Webhook> CreateWebhookAsync(CreateWebhookRequest request)
        {
            var json = JsonConvert.SerializeObject(request, _defaultJsonSerializerSettings);
            var result = await _httpClientProxy.PostAsync("/webhooks", json).ConfigureAwait(false);
            var webhook = JsonConvert.DeserializeObject<Webhook>(result, _defaultJsonSerializerSettings);
            return webhook;
        }

        public async Task DeleteWebhookAsync(string webhookId)
        {
            await _httpClientProxy.DeleteAsync($"/webhooks/{webhookId}");
        }
    }
}
