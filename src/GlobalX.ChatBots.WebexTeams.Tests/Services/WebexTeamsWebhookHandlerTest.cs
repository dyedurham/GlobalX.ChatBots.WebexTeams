using System.Linq;
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
using Shouldly;
using TestStack.BDDfy;
using Xunit;
using GlobalXMessage = GlobalX.ChatBots.Core.Messages.Message;
using GlobalXPerson = GlobalX.ChatBots.Core.People.Person;
using WebexTeamsMessage = GlobalX.ChatBots.WebexTeams.Models.Message;
using WebexTeamsPerson = GlobalX.ChatBots.WebexTeams.Models.Person;

namespace GlobalX.ChatBots.WebexTeams.Tests.Services
{
    public class WebexTeamsWebhookHandlerTest
    {
        // mocks
        private readonly IWebexTeamsApiService _apiService;
        private readonly WebexTeamsSettings _settings;
        private readonly IWebexTeamsMessageParser _messageParser;

        // subject
        private readonly WebexTeamsWebhookHandler _subject;

        private string _callbackBody;
        private GlobalXMessage _result;

        public WebexTeamsWebhookHandlerTest()
        {
            _apiService = Substitute.For<IWebexTeamsApiService>();
            _settings = new WebexTeamsSettings();
            _messageParser = Substitute.For<IWebexTeamsMessageParser>();
            var options = Substitute.For<IOptions<WebexTeamsSettings>>();
            options.Value.Returns(_settings);
            var mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<CommonMappers>();
                c.AddProfile<WebhookMapper>();
                c.AddProfile<PersonMapper>();
            }).CreateMapper();
            _subject = new WebexTeamsWebhookHandler(_apiService, options, new WebexTeamsMapper(mapper), _messageParser);
        }

        [Theory]
        [MemberData(nameof(WebexTeamsWebhookHandlerTestData.SuccessfulRegisterHooksTestData), MemberType =
            typeof(WebexTeamsWebhookHandlerTestData))]
        internal void TestSuccessfullyRegisterHooks(WebexTeamsSettings settings, Models.Webhook[] existingHooks,
            CreateWebhookRequest[] mappedHooks)
        {
            this.Given(x => GivenWebexTeamsSettings(settings))
                .And(x => GivenExistingWebhooks(existingHooks))
                .When(x => WhenRegisteringWebhooks())
                .Then(x => ThenItShouldDeleteExistingWebhooks(existingHooks))
                .And(x => ThenItShouldRegisterTheNewWebhooks(mappedHooks))
                .BDDfy();
        }

        [Theory]
        [MemberData(nameof(WebexTeamsWebhookHandlerTestData.SuccessfulProcessMessageWebhookCallbackTestData),
            MemberType = typeof(WebexTeamsWebhookHandlerTestData))]
        internal void TestProcessMessageWebhookCallback(string body, string messageId, WebexTeamsMessage apiResponse, WebexTeamsPerson sender,
            GlobalXMessage output)
        {
            this.Given(x => GivenACallbackBody(body))
                .When(x => WhenProcessingAWebhookCallback(messageId, apiResponse, output, sender))
                .Then(x => ThenItShouldReturnTheGlobalXMessage(output))
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

        private void GivenACallbackBody(string body)
        {
            _callbackBody = body;
        }

        private async void WhenRegisteringWebhooks()
        {
            await _subject.RegisterWebhooksAsync();
        }

        private async void WhenProcessingAWebhookCallback(string messageId, WebexTeamsMessage apiResponse,
            GlobalXMessage parsedMessage, WebexTeamsPerson sender)
        {
            _apiService.GetMessageAsync(messageId).Returns(Task.FromResult(apiResponse));
            _messageParser
                .ParseMessage(Arg.Is<WebexTeamsMessage>(x => x.Id == apiResponse.Id && x.Html == apiResponse.Html))
                .Returns(parsedMessage);
            _apiService.GetPersonAsync(apiResponse.PersonId).Returns(Task.FromResult(sender));
            _result = await _subject.ProcessMessageWebhookCallbackAsync(_callbackBody);
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

        private void ThenItShouldReturnTheGlobalXMessage(GlobalXMessage result)
        {
            _result.ShouldNotBeNull();
            _result.ShouldSatisfyAllConditions(
                () => _result.Created.ShouldBe(result.Created),
                () => _result.Text.ShouldBe(result.Text),
                () =>
                {
                    if (result.MessageParts != null)
                    {
                        _result.MessageParts.ShouldNotBeNull();
                        _result.MessageParts.Length.ShouldBe(result.MessageParts.Length);
                        for (int i = 0; i < _result.MessageParts.Length; i++)
                        {
                            _result.MessageParts[i].MessageType.ShouldBe(result.MessageParts[i].MessageType);
                            _result.MessageParts[i].Text.ShouldBe(result.MessageParts[i].Text);
                            _result.MessageParts[i].UserId.ShouldBe(result.MessageParts[i].UserId);
                        }
                    }
                    else
                    {
                        _result.MessageParts.ShouldBeNull();
                    }
                },
                () =>
                {
                    if (result.Sender != null)
                    {
                        ComparePeople(result.Sender, _result.Sender);
                    }
                    else
                    {
                        _result.Sender.ShouldBeNull();
                    }
                },
                () => _result.RoomId.ShouldBe(result.RoomId),
                () => _result.RoomType.ShouldBe(result.RoomType)
            );
        }

        private static void ComparePeople(GlobalXPerson expected, GlobalXPerson actual)
        {
            actual.ShouldNotBeNull();
            actual.ShouldSatisfyAllConditions(
                () => actual.Created.ShouldBe(expected.Created),
                () => actual.Username.ShouldBe(expected.Username),
                () => actual.UserId.ShouldBe(expected.UserId),
                () =>
                {
                    if (expected.Emails != null)
                    {
                        actual.Emails.ShouldNotBeNull();
                        actual.Emails.OrderBy(x => x).SequenceEqual(expected.Emails.OrderBy(x => x)).ShouldBe(true);
                    }
                    else
                    {
                        actual.Emails.ShouldBeNull();
                    }
                },
                () => actual.Type.ShouldBe(expected.Type)
            );
        }
    }
}
