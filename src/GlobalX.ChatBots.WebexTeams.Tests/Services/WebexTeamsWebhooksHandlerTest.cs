using System.Threading.Tasks;
using AutoMapper;
using GlobalX.ChatBots.WebexTeams.Configuration;
using GlobalX.ChatBots.WebexTeams.Mappers;
using GlobalX.ChatBots.WebexTeams.Mappers.Profiles;
using GlobalX.ChatBots.WebexTeams.Models;
using GlobalX.ChatBots.WebexTeams.Services;
using GlobalX.ChatBots.WebexTeams.Tests.TestData;
using Microsoft.Extensions.Options;
using NSubstitute;
using TestStack.BDDfy;
using Xunit;

namespace GlobalX.ChatBots.WebexTeams.Tests.Services
{
    public class WebexTeamsWebhooksHandlerTest
    {
        // mocks
        private readonly IWebexTeamsApiService _apiService;
        private readonly WebexTeamsSettings _settings;
        private readonly IWebexTeamsMessageParser _messageParser;

        // subject
        private readonly WebexTeamsWebhookHandler _subject;

        public WebexTeamsWebhooksHandlerTest()
        {
            _apiService = Substitute.For<IWebexTeamsApiService>();
            _settings = new WebexTeamsSettings();
            _messageParser = Substitute.For<IWebexTeamsMessageParser>();
            var options = Substitute.For<IOptions<WebexTeamsSettings>>();
            options.Value.Returns(_settings);
            var mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<WebhookMapper>();
            }).CreateMapper();
            _subject = new WebexTeamsWebhookHandler(_apiService, options, new WebexTeamsMapper(mapper), _messageParser);
        }

        [Theory]
        [MemberData(nameof(WebexTeamsWebhookHandlerTestData.SuccessfulRegisterHooksTestData), MemberType = typeof(WebexTeamsWebhookHandlerTestData))]
        internal void TestSuccessfullyRegisterHooks(WebexTeamsSettings settings, Models.Webhook[] existingHooks, CreateWebhookRequest[] mappedHooks)
        {
            this.Given(x => GivenWebexTeamsSettings(settings))
                .And(x => GivenExistingWebhooks(existingHooks))
                .When(x => WhenRegisteringWebhooks())
                .Then(x => ThenItShouldDeleteExistingWebhooks(existingHooks))
                .And(x => ThenItShouldRegisterTheNewWebhooks(mappedHooks))
                .BDDfy();
        }

        private void GivenWebexTeamsSettings(WebexTeamsSettings settings)
        {
            _settings.BotAuthToken = settings.BotAuthToken;
            _settings.WebexTeamsApiUrl = settings.WebexTeamsApiUrl;
            _settings.Webhooks = settings.Webhooks;
        }

        private void GivenExistingWebhooks(Models.Webhook[] existingHooks)
        {
            _apiService.GetWebhooksAsync().Returns(Task.FromResult(existingHooks));
        }

        private async void WhenRegisteringWebhooks()
        {
            await _subject.RegisterWebhooksAsync();
        }

        private void ThenItShouldDeleteExistingWebhooks(Models.Webhook[] existingHooks)
        {
            _apiService.ReceivedWithAnyArgs(existingHooks.Length).DeleteWebhookAsync(Arg.Any<string>());
            foreach (var hook in existingHooks)
            {
                _apiService.Received(1).DeleteWebhookAsync(hook.Id);
            }
        }

        private void ThenItShouldRegisterTheNewWebhooks(CreateWebhookRequest[] mappedHooks)
        {
            _apiService.ReceivedWithAnyArgs(mappedHooks.Length).CreateWebhookAsync(Arg.Any<CreateWebhookRequest>());
            foreach (var hook in mappedHooks)
            {
                _apiService.Received(1).CreateWebhookAsync(Arg.Is<CreateWebhookRequest>(x =>
                    x.Name == hook.Name && x.TargetUrl == hook.TargetUrl && x.Resource == hook.Resource &&
                    x.Event == hook.Event && x.Filter == hook.Filter && x.Secret == hook.Secret));
            }
        }
    }
}
