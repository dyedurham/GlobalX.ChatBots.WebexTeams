using GlobalX.ChatBots.Core;
using GlobalX.ChatBots.Core.Messages;
using GlobalX.ChatBots.Core.People;
using GlobalX.ChatBots.Core.Rooms;
using GlobalX.ChatBots.WebexTeams.Configuration;
using GlobalX.ChatBots.WebexTeams.Mappers;
using GlobalX.ChatBots.WebexTeams.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GlobalX.ChatBots.WebexTeams
{
    public static class Extensions
    {
        public static IServiceCollection ConfigureWebexTeamsBot(this IServiceCollection services,
            WebexTeamsSettings settings)
        {
            var options = new OptionsWrapper<WebexTeamsSettings>(settings);
            services.AddSingleton<IOptions<WebexTeamsSettings>>(options);
            services.ConfigureCommonServices();
            return services;
        }

        public static IServiceCollection ConfigureWebexTeamsBot(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<WebexTeamsSettings>(configuration.GetSection("GlobalX.ChatBots.WebexTeams"));
            services.ConfigureCommonServices();
            return services;
        }

        private static IServiceCollection ConfigureCommonServices(this IServiceCollection services)
        {
            services.AddHttpClient<IHttpClientProxy, HttpClientProxy>();
            services.AddTransient<IWebexTeamsApiService, WebexTeamsApiService>();
            services.AddTransient<IChatHelper, WebexTeamsChatHelper>();
            services.AddTransient<IWebhookHelper, WebexTeamsChatHelper>();
            services.AddTransient<IMessageHandler, WebexTeamsMessageHandler>();
            services.AddTransient<IPersonHandler, WebexTeamsPersonHandler>();
            services.AddTransient<IRoomHandler, WebexTeamsRoomHandler>();
            services.AddTransient<IWebexTeamsMessageParser, WebexTeamsMessageParser>();
            services.AddTransient<IWebexTeamsWebhookHandler, WebexTeamsWebhookHandler>();
            services.AddSingleton(WebexTeamsMapperFactory.CreateMapper());
            return services;
        }
    }
}
