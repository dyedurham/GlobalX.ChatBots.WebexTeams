using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GlobalX.ChatBots.Core.Messages;
using GlobalX.ChatBots.WebexTeams.Mappers;
using GlobalX.ChatBots.WebexTeams.Mappers.Profiles;
using GlobalX.ChatBots.WebexTeams.Models;
using GlobalX.ChatBots.WebexTeams.Services;
using GlobalX.ChatBots.WebexTeams.Tests.TestData;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Shouldly;
using TestStack.BDDfy;
using Xunit;
using GlobalXMessage = GlobalX.ChatBots.Core.Messages.Message;
using GlobalXPerson = GlobalX.ChatBots.Core.People.Person;
using WebexTeamsMessage = GlobalX.ChatBots.WebexTeams.Models.Message;

namespace GlobalX.ChatBots.WebexTeams.Tests.Services
{
    public class WebexTeamsMessageHandlerTest
    {
        private GlobalXMessage _input;
        private GlobalXMessage _output;

        private WebexTeamsMessageHandler _subject;

        // mocks
        private readonly IWebexTeamsApiService _apiService;
        private readonly IWebexTeamsMessageParser _messageParser;

        public WebexTeamsMessageHandlerTest()
        {
            _apiService = Substitute.For<IWebexTeamsApiService>();
            _messageParser = Substitute.For<IWebexTeamsMessageParser>();
            var mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<CommonMappers>();
                c.AddProfile<PersonMapper>();
            }).CreateMapper();
            _subject = new WebexTeamsMessageHandler(_apiService, _messageParser, new WebexTeamsMapper(mapper));
        }

        [Theory]
        [MemberData(nameof(WebexTeamsMessageHandlerTestData.SuccessfulSendMessageTestData),
            MemberType = typeof(WebexTeamsMessageHandlerTestData))]
        internal void TestSendMessage(GlobalXMessage input, CreateMessageRequest parsedInput,
            WebexTeamsMessage apiResponse, GlobalXMessage parsedApiResponse, Person sender, GlobalXMessage output)
        {
            this.Given(x => GivenAGlobalXMessage(input))
                .And(x => GivenServicesReturnExpectedValues(parsedInput, apiResponse, parsedApiResponse, sender))
                .When(x => WhenSendingAMessage())
                .Then(x => ThenItShouldCallApiService())
                .And(x => ThenItShouldReturnAMessage(output))
                .BDDfy();
        }

        [Theory]
        [MemberData(nameof(WebexTeamsMessageHandlerTestData.BadParentMessageTestData),
            MemberType = typeof(WebexTeamsMessageHandlerTestData))]
        internal void TestSendBadParentMessage(GlobalXMessage input, WebexTeamsMessage apiResponse)
        {
            this.Given(x => GivenAGlobalXMessage(input))
                .And(x => GivenApiServiceThrowsInvalidParentExceptionForBadParent())
                .And(x => GivenMessageParserReturnsCreateMessageRequest())
                .And(x => GivenApiServiceReturnsGoodParent())
                .And(x => GivenApiServiceReturnsCorrectResult(apiResponse))
                .And(x => GivenServicesReturnSomeValues())
                .When(x => WhenSendingAMessage())
                .Then(x => ThenThereShouldBeTwoCallsToSendMessage())
                .BDDfy();
        }

        private void GivenAGlobalXMessage(GlobalXMessage input)
        {
            _input = input;
        }

        private void GivenServicesReturnExpectedValues(CreateMessageRequest parsedInput, WebexTeamsMessage apiResponse,
            GlobalXMessage parsedApiResponse, Person sender)
        {
            _messageParser.ParseCreateMessageRequest(_input).Returns(parsedInput);
            _apiService.SendMessageAsync(parsedInput).Returns(Task.FromResult(apiResponse));
            _messageParser.ParseMessage(apiResponse).Returns(parsedApiResponse);
            _apiService.GetPersonAsync(apiResponse.PersonId).Returns(Task.FromResult(sender));
        }

        private void GivenApiServiceThrowsInvalidParentExceptionForBadParent()
        {
            _apiService.SendMessageAsync(Arg.Is<CreateMessageRequest>(x => x.ParentId == "badParentId")).Throws(new InvalidParentException());
        }

        private void GivenMessageParserReturnsCreateMessageRequest()
        {
            _messageParser.ParseCreateMessageRequest(Arg.Any<GlobalX.ChatBots.Core.Messages.Message>())
                .ReturnsForAnyArgs(x => new CreateMessageRequest
                {
                    ParentId = ((GlobalX.ChatBots.Core.Messages.Message) x[0]).ParentId
                });
        }

        private void GivenApiServiceReturnsGoodParent()
        {
            _apiService.GetMessageAsync(Arg.Is("badParentId")).Returns(Task.FromResult(new WebexTeamsMessage
            {
                Id = "goodParentId"
            }));
        }

        private void GivenApiServiceReturnsCorrectResult(WebexTeamsMessage apiResponse)
        {
            _apiService.SendMessageAsync(Arg.Is<CreateMessageRequest>(x => x.ParentId == "goodParentId"))
                .Returns(apiResponse);
        }

        private void GivenServicesReturnSomeValues()
        {
            _messageParser.ParseMessage(Arg.Is<WebexTeamsMessage>(x => x.ParentId == "goodParentId")).Returns(
                new GlobalX.ChatBots.Core.Messages.Message
                {
                    ParentId = "goodParentId",
                    Sender = new GlobalXPerson()
                });
        }

        private async void WhenSendingAMessage()
        {
            _output = await _subject.SendMessageAsync(_input);
        }

        private void ThenItShouldCallApiService()
        {
            _apiService.ReceivedWithAnyArgs(1).SendMessageAsync(Arg.Any<CreateMessageRequest>());
        }

        private void ThenItShouldReturnAMessage(GlobalXMessage output)
        {
            _output.ShouldNotBeNull();
            _output.ShouldSatisfyAllConditions(
                () => _output.Created.ShouldBe(output.Created),
                () => _output.Text.ShouldBe(output.Text),
                () => {
                    if (output.MessageParts != null)
                    {
                        _output.MessageParts.ShouldNotBeNull();
                        _output.MessageParts.Length.ShouldBe(output.MessageParts.Length);
                        for (int i = 0; i < _output.MessageParts.Length; i++)
                        {
                            _output.MessageParts[i].MessageType.ShouldBe(output.MessageParts[i].MessageType);
                            _output.MessageParts[i].Text.ShouldBe(output.MessageParts[i].Text);
                            _output.MessageParts[i].UserId.ShouldBe(output.MessageParts[i].UserId);
                        }
                    }
                    else
                    {
                        _output.MessageParts.ShouldBeNull();
                    }
                },
                () =>
                {
                    if (output.Sender != null)
                    {
                        ComparePeople(output.Sender, _output.Sender);
                    }
                    else
                    {
                        _output.Sender.ShouldBeNull();
                    }
                },
                () => _output.RoomId.ShouldBe(output.RoomId),
                () => _output.RoomType.ShouldBe(output.RoomType)
            );
        }

        private void ThenThereShouldBeTwoCallsToSendMessage()
        {
            _apiService.Received()
                .SendMessageAsync(Arg.Is<CreateMessageRequest>(x => x.ParentId == "badParentId"));
            _apiService.Received()
                .SendMessageAsync(Arg.Is<CreateMessageRequest>(x => x.ParentId == "goodParentId"));
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
