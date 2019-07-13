using System;
using System.Net.Http;
using GlobalX.ChatBots.Core;
using GlobalX.ChatBots.WebexTeams.Mappers;
using GlobalX.ChatBots.WebexTeams.Services;

namespace GlobalX.ChatBots.WebexTeams
{
    public class WebexTeamsChatHelperFactory
    {
        public IChatHelper CreateWebexTeamsChatHelper()
        {
            var httpClient = new HttpClient{
                BaseAddress = new Uri("https://api.ciscospark.com/v1")
            };
            var httpClientProxy = new HttpClientProxy(httpClient);
            var apiService = new WebexTeamsApiService(httpClientProxy);

            var mapper = WebexTeamsMapperFactory.CreateMapper();
            var messageParser = new WebexTeamsMessageParser(mapper);

            var messageHandler = new WebexTeamsMessageHandler(apiService, messageParser);
            var personHandler = new WebexTeamsPersonHandler(apiService, mapper);
            var roomHandler = new WebexTeamsRoomHandler();

            return new WebexTeamsChatHelper(messageHandler, personHandler, roomHandler);
        }
    }
}
