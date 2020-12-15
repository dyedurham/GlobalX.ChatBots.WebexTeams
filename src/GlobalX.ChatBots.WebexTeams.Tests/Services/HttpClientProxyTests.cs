using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GlobalX.ChatBots.WebexTeams.Configuration;
using GlobalX.ChatBots.WebexTeams.Services;
using GlobalX.ChatBots.WebexTeams.Tests.TestServices;
using Microsoft.Extensions.Options;
using NSubstitute;
using Shouldly;
using TestStack.BDDfy;
using Xunit;

namespace GlobalX.ChatBots.WebexTeams.Tests.Services
{
    public class HttpClientProxyTests
    {
        private readonly HttpClient _httpClient;
        private readonly MockHttpMessageHandler _messageHandler;
        private IHttpClientProxy _subject;

        private string _path;
        private string _body;

        private Exception _exception;
        private string _response;

        public HttpClientProxyTests()
        {
            _messageHandler = new MockHttpMessageHandler();
            _httpClient = new HttpClient(_messageHandler);
            var settingsContainer = Substitute.For<IOptions<WebexTeamsSettings>>();
            var settings = new WebexTeamsSettings
            {
                BotAuthToken = "botAuthToken",
                WebexTeamsApiUrl = "https://localhost:1234"
            };
            settingsContainer.Value.Returns(settings);
            _subject = new HttpClientProxy(_httpClient, settingsContainer);
        }

        [Fact]
        public void PostRequestShouldSendRequest()
        {
            this.Given(x => GivenAPath())
                .And(x => GivenABody())
                .And(x => GivenResponseIsValid())
                .When(x => WhenPostAsyncIsCalled())
                .Then(x => ThenNoExceptionShouldBeThrown())
                .And(x => ThenHttpClientShouldReceiveARequest())
                .BDDfy();
        }

        [Fact]
        public void BadRequestPostRequestShouldThrowInvalidParentException()
        {

        }

        [Fact]
        public void ErrorInPostRequestShouldThrowHttpRequestException()
        {

        }

        private void GivenAPath()
        {
            _path = "/api/endpoint";
        }

        private void GivenABody()
        {
            _body = "{\"key\": \"value\"}";
        }

        private void GivenResponseIsValid()
        {
            _messageHandler.SetResponse(HttpStatusCode.OK, "OK");
        }

        private async void WhenPostAsyncIsCalled()
        {
            _exception = await Record.ExceptionAsync(async () => _response = await _subject.PostAsync(_path, _body));
        }

        private void ThenNoExceptionShouldBeThrown()
        {
            _exception.ShouldBeNull();
        }

        private void ThenHttpClientShouldReceiveARequest()
        {
            _messageHandler.Inputs.Count.ShouldBe(1);
            _messageHandler.Inputs[0].ShouldNotBeNull();
        }
    }
}
