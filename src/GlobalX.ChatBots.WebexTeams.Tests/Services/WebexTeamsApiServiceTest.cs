using System;
using System.Linq;
using GlobalX.ChatBots.WebexTeams.Models;
using GlobalX.ChatBots.WebexTeams.Services;
using GlobalX.ChatBots.WebexTeams.Tests.TestData;
using NSubstitute;
using Shouldly;
using TestStack.BDDfy;
using Xunit;

namespace GlobalX.ChatBots.WebexTeams.Tests.Services {
    public class WebexTeamsApiServiceTest
    {
        private string _messageId;
        private CreateMessageRequest _createMessageRequest;
        private Message _messageResponse;

        private readonly WebexTeamsApiService _subject;

        // mocks
        private IHttpClientProxy _httpClientProxy;

        public WebexTeamsApiServiceTest()
        {
            _httpClientProxy = Substitute.For<IHttpClientProxy>();
            _subject = new WebexTeamsApiService(_httpClientProxy);
        }

        [Theory]
        [MemberData(nameof(WebexTeamsApiServiceTestData.GetMessageTestData), MemberType = typeof(WebexTeamsApiServiceTestData))]
        internal void TestGetMessage(string messageId, string httpResponse, Message response)
        {
            this.Given(x => GivenAMessageId(messageId))
                .When(x => WhenGettingAMessage(httpResponse))
                .Then(x => ThenItShouldCallTheHttpClientGet())
                .And(x => ThenItShouldReturnTheMessage(response))
                .BDDfy();
        }

        [Theory]
        [MemberData(nameof(WebexTeamsApiServiceTestData.SendMessageTestData), MemberType = typeof(WebexTeamsApiServiceTestData))]
        internal void TestSendMessage(CreateMessageRequest request, string httpResponse, Message response)
        {
            this.Given(x => GivenACreateMessageRequest(request))
                .When(x => WhenSendingAMessage(httpResponse))
                .Then(x => ThenItShouldCallTheHttpClientPost())
                .And(x => ThenItShouldReturnTheMessage(response))
                .BDDfy();
        }

        private void GivenAMessageId(string messageId)
        {
            _messageId = messageId;
        }

        private void GivenACreateMessageRequest(CreateMessageRequest createMessageRequest)
        {
            _createMessageRequest = createMessageRequest;
        }

        private async void WhenGettingAMessage(string response)
        {
            _httpClientProxy.GetAsync(Arg.Any<string>()).Returns(response);
            _messageResponse = await _subject.GetMessageAsync(_messageId);
        }

        private async void WhenSendingAMessage(string response)
        {
            _httpClientProxy.PostAsync(Arg.Any<string>(), Arg.Any<string>()).Returns(response);
            _messageResponse = await _subject.SendMessageAsync(_createMessageRequest);
        }

        private void ThenItShouldCallTheHttpClientGet()
        {
            _httpClientProxy.ReceivedWithAnyArgs(1).GetAsync(Arg.Any<string>());
        }

        private void ThenItShouldCallTheHttpClientPost()
        {
            _httpClientProxy.ReceivedWithAnyArgs(1).PostAsync(Arg.Any<string>(), Arg.Any<string>());
        }

        private void ThenItShouldReturnTheMessage(Message message)
        {
            _messageResponse.ShouldNotBeNull();
            _messageResponse.ShouldSatisfyAllConditions(
                () => _messageResponse.Id.ShouldBe(message.Id),
                () => _messageResponse.RoomId.ShouldBe(message.RoomId),
                () => _messageResponse.RoomType.ShouldBe(message.RoomType),
                () => _messageResponse.ToPersonId.ShouldBe(message.ToPersonId),
                () => _messageResponse.ToPersonEmail.ShouldBe(message.ToPersonEmail),
                () => _messageResponse.Text.ShouldBe(message.Text),
                () =>_messageResponse.Markdown.ShouldBe(message.Markdown),
                () => _messageResponse.Html.ShouldBe(message.Html),
                () => {
                    if (message.Files != null)
                    {
                        _messageResponse.Files.ShouldNotBeNull();
                        _messageResponse.Files.OrderBy(x => x).SequenceEqual(message.Files.OrderBy(x => x)).ShouldBeTrue();
                    }
                    else
                    {
                        _messageResponse.Files.ShouldBeNull();
                    }
                },
                () => _messageResponse.PersonId.ShouldBe(message.PersonId),
                () => _messageResponse.PersonEmail.ShouldBe(message.PersonEmail),
                () => {
                    if (message.MentionedPeople != null)
                    {
                        _messageResponse.MentionedPeople.ShouldNotBeNull();
                        _messageResponse.MentionedPeople.OrderBy(x => x).SequenceEqual(message.MentionedPeople.OrderBy(x => x)).ShouldBeTrue();
                    }
                    else
                    {
                        _messageResponse.MentionedPeople.ShouldBeNull();
                    }
                },
                () => {
                    if (message.MentionedGroups != null)
                    {
                        _messageResponse.MentionedGroups.ShouldNotBeNull();
                        _messageResponse.MentionedGroups.OrderBy(x => x).SequenceEqual(message.MentionedGroups.OrderBy(x => x)).ShouldBeTrue();
                    }
                    else
                    {
                        _messageResponse.MentionedGroups.ShouldBeNull();
                    }
                },
                () => _messageResponse.Created.ShouldBe(message.Created)
            );
        }
    }
}
