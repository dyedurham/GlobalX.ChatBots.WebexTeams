using System;
using System.Net.Http;
using GlobalX.ChatBots.WebexTeams.Configuration;
using GlobalX.ChatBots.WebexTeams.Mappers;
using GlobalX.ChatBots.WebexTeams.Services;
using Microsoft.Extensions.Options;

namespace GlobalX.ChatBots.WebexTeams
{
    public static class WebexTeamsChatHelperFactory
    {
        public static WebexTeamsChatHelper CreateWebexTeamsChatHelper(WebexTeamsSettings settings)
        {
            var httpClient = new HttpClient{
                BaseAddress = new Uri("https://api.ciscospark.com/v1")
            };
            var httpClientProxy = new HttpClientProxy(httpClient, new OptionsWrapper<WebexTeamsSettings>(settings));
            var apiService = new WebexTeamsApiService(httpClientProxy);

            var mapper = WebexTeamsMapperFactory.CreateMapper();
            var messageParser = new WebexTeamsMessageParser(mapper);

            var messageHandler = new WebexTeamsMessageHandler(apiService, messageParser);
            var personHandler = new WebexTeamsPersonHandler(apiService, mapper);
            var roomHandler = new WebexTeamsRoomHandler(apiService, mapper);

            var webhooks = new WebexTeamsWebhookHandler(apiService, new OptionsWrapper<WebexTeamsSettings>(settings),
                mapper, messageParser);

            return new WebexTeamsChatHelper(messageHandler, personHandler, roomHandler, webhooks);
        }
    }
}
