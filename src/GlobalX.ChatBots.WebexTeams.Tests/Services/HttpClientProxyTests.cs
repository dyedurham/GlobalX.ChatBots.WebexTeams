using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GlobalX.ChatBots.WebexTeams.Configuration;
using GlobalX.ChatBots.WebexTeams.Models;
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
            this.Given(x => GivenAPath())
                .And(x => GivenABody())
                .And(x => GivenResponseIsBadRequest())
                .When(x => WhenPostAsyncIsCalled())
                .Then(x => ThenInvalidParentExceptionShouldBeThrown())
                .BDDfy();
        }

        [Fact]
        public void ErrorInPostRequestShouldThrowHttpRequestException()
        {
            this.Given(x => GivenAPath())
                .And(x => GivenABody())
                .And(x => GivenResponseIsUnsuccessful())
                .When(x => WhenPostAsyncIsCalled())
                .Then(x => ThenHttpRequestExceptionShouldBeThrown())
                .BDDfy();
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

        private void GivenResponseIsBadRequest()
        {
            _messageHandler.SetResponse(HttpStatusCode.BadRequest, "Bad Request");
        }

        private void GivenResponseIsUnsuccessful()
        {
            _messageHandler.SetResponse(HttpStatusCode.InternalServerError, "Internal Server Error");
        }

        private async void WhenPostAsyncIsCalled()
        {
            _exception = await Record.ExceptionAsync(async () => _response = await _subject.PostAsync(_path, _body));
        }

        private void ThenNoExceptionShouldBeThrown()
        {
            _exception.ShouldBeNull();
        }

        private void ThenInvalidParentExceptionShouldBeThrown()
        {
            _exception.ShouldNotBeNull();
            _exception.ShouldBeOfType<InvalidParentException>();
        }

        private void ThenHttpRequestExceptionShouldBeThrown()
        {
            _exception.ShouldNotBeNull();
            _exception.ShouldBeOfType<HttpRequestException>();
        }

        private void ThenHttpClientShouldReceiveARequest()
        {
            _messageHandler.Inputs.Count.ShouldBe(1);
            _messageHandler.Inputs[0].ShouldNotBeNull();
        }
    }
}
