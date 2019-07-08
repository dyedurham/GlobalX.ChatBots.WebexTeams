using System;
using System.Collections.Generic;
using System.Text;
using GlobalX.ChatBots.Core;
using GlobalX.ChatBots.Core.Messages;
using GlobalX.ChatBots.Core.People;
using GlobalX.ChatBots.Core.Rooms;
using GlobalX.ChatBots.WebexTeams.Mappers;
using GlobalX.ChatBots.WebexTeams.Messages;
using GlobalX.ChatBots.WebexTeams.People;
using GlobalX.ChatBots.WebexTeams.Rooms;
using GlobalX.ChatBots.WebexTeams.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GlobalX.ChatBots.WebexTeams
{
    public static class Extensions
    {
        public static IServiceCollection ConfigureWebexTeamsBot(this IServiceCollection services)
        {
            services.AddHttpClient<IHttpClientProxy, HttpClientProxy>();
            services.AddScoped<IWebexTeamsApiService, WebexTeamsApiService>();
            services.AddScoped<IChatHelper, WebexChatHelper>();
            services.AddScoped<IMessageHandler, WebexMessageHandler>();
            services.AddScoped<IPersonHandler, WebexPersonHandler>();
            services.AddScoped<IRoomHandler, WebexRoomHandler>();
            services.AddSingleton<IWebexTeamsMapper>((IWebexTeamsMapper)WebexTeamsMapperFactory.CreateMapper());
            return services;
        }
    }
}
