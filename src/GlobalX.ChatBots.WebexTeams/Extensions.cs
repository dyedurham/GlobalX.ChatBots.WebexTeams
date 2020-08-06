using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
using Polly;
using Polly.Extensions.Http;

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
            services.AddHttpClient<IHttpClientProxy, HttpClientProxy>()
                .AddPolicyHandler(GetRetryPolicy()); ;
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

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == (HttpStatusCode) 429)
                .WaitAndRetryAsync(6,
                    (retryAttempt, response, context) =>
                    {
                        if (response.Exception != null)
                        {
                            return TimeSpan.FromSeconds(0.5 * Math.Pow(2, retryAttempt));
                        }

                        var msg = response.Result;
                        return msg.Headers.RetryAfter.Delta ?? TimeSpan.Zero;
                    },
                    (e, ts, i, ctx) => Task.CompletedTask);
        }
    }
}
