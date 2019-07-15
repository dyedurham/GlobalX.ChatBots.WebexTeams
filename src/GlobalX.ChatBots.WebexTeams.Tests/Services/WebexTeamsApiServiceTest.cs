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
        private string _personId;
        private string _roomId;
        private CreateMessageRequest _createMessageRequest;
        private Message _messageResponse;
        private Person _personResponse;
        private Room _roomResponse;

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

        [Theory]
        [MemberData(nameof(WebexTeamsApiServiceTestData.GetPersonTestData), MemberType = typeof(WebexTeamsApiServiceTestData))]
        internal void TestGetPerson(string personId, string httpResponse, Person response)
        {
            this.Given(x => GivenAPersonId(personId))
                .When(x => WhenGettingAPerson(httpResponse))
                .Then(x => ThenItShouldCallTheHttpClientGet())
                .And(x => ThenItShouldReturnThePerson(response))
                .BDDfy();
        }

        [Theory]
        [MemberData(nameof(WebexTeamsApiServiceTestData.GetRoomTestData), MemberType = typeof(WebexTeamsApiServiceTestData))]
        internal void TestGetRoom(string roomId, string httpResponse, Room response)
        {
            this.Given(x => GivenARoomId(roomId))
                .When(x => WhenGettingARoom(httpResponse))
                .Then(x => ThenItShouldCallTheHttpClientGet())
                .And(x => ThenItShouldReturnTheRoom(response))
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

        private void GivenAPersonId(string personId)
        {
            _personId = personId;
        }

        private void GivenARoomId(string roomId)
        {
            _roomId = roomId;
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

        private async void WhenGettingAPerson(string response)
        {
            _httpClientProxy.GetAsync(Arg.Any<string>()).Returns(response);
            _personResponse = await _subject.GetPersonAsync(_personId);
        }

        private async void WhenGettingARoom(string response)
        {
            _httpClientProxy.GetAsync(Arg.Any<string>()).Returns(response);
            _roomResponse = await _subject.GetRoomAsync(_roomId);
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
                () =>
                {
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
                () =>
                {
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
                () =>
                {
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

        private void ThenItShouldReturnTheRoom(Room room)
        {
            _roomResponse.ShouldNotBeNull();
            _roomResponse.ShouldSatisfyAllConditions(
                () => _roomResponse.Id.ShouldBe(room.Id),
                () => _roomResponse.Title.ShouldBe(room.Title),
                () => _roomResponse.Type.ShouldBe(room.Type),
                () => _roomResponse.IsLocked.ShouldBe(room.IsLocked),
                () => _roomResponse.TeamId.ShouldBe(room.TeamId),
                () => _roomResponse.LastActivity.ShouldBe(room.LastActivity),
                () => _roomResponse.CreatorId.ShouldBe(room.CreatorId),
                () => _roomResponse.Created.ShouldBe(room.Created),
                () => _roomResponse.SipAddress.ShouldBe(room.SipAddress)
            );
        }

        private void ThenItShouldReturnThePerson(Person person)
        {
            _personResponse.ShouldNotBeNull();
            _personResponse.ShouldSatisfyAllConditions(
                () => _personResponse.Id.ShouldBe(person.Id),
                () =>
                {
                    if (person.Emails != null)
                    {
                        _personResponse.Emails.ShouldNotBeNull();
                        _personResponse.Emails.OrderBy(x => x).SequenceEqual(person.Emails.OrderBy(x => x)).ShouldBeTrue();
                    }
                    else
                    {
                        _personResponse.Emails.ShouldBeNull();
                    }
                },
                () =>
                {
                    if (person.PhoneNumbers != null)
                    {
                        _personResponse.PhoneNumbers.ShouldNotBeNull();
                        _personResponse.PhoneNumbers.OrderBy(x => x).SequenceEqual(person.PhoneNumbers.OrderBy(x => x)).ShouldBeTrue();
                    }
                    else
                    {
                        _personResponse.PhoneNumbers.ShouldBeNull();
                    }
                },
                () => _personResponse.DisplayName.ShouldBe(person.DisplayName),
                () => _personResponse.NickName.ShouldBe(person.NickName),
                () => _personResponse.FirstName.ShouldBe(person.FirstName),
                () => _personResponse.LastName.ShouldBe(person.LastName),
                () => _personResponse.Avatar.ShouldBe(person.Avatar),
                () => _personResponse.OrgId.ShouldBe(person.OrgId),
                () =>
                {
                    if (person.Roles != null)
                    {
                        _personResponse.Roles.ShouldNotBeNull();
                        _personResponse.Roles.OrderBy(x => x).SequenceEqual(person.Roles.OrderBy(x => x)).ShouldBeTrue();
                    }
                    else
                    {
                        _personResponse.Roles.ShouldBeNull();
                    }
                },
                () =>
                {
                    if (person.Licenses != null)
                    {
                        _personResponse.Licenses.ShouldNotBeNull();
                        _personResponse.Licenses.OrderBy(x => x).SequenceEqual(person.Licenses.OrderBy(x => x)).ShouldBeTrue();
                    }
                    else
                    {
                        _personResponse.Licenses.ShouldBeNull();
                    }
                },
                () => _personResponse.Created.ShouldBe(person.Created),
                () => _personResponse.LastModified.ShouldBe(person.LastModified),
                () => _personResponse.Timezone.ShouldBe(person.Timezone),
                () => _personResponse.LastActivity.ShouldBe(person.LastActivity),
                () => _personResponse.Status.ShouldBe(person.Status),
                () => _personResponse.InvitePending.ShouldBe(person.InvitePending),
                () => _personResponse.LoginEnabled.ShouldBe(person.LoginEnabled),
                () => _personResponse.Type.ShouldBe(person.Type)
            );
        }
    }
}
