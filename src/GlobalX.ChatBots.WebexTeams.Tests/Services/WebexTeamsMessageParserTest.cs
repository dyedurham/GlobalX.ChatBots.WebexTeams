using System;
using System.Linq;
using AutoMapper;
using GlobalX.ChatBots.WebexTeams.Mappers;
using GlobalX.ChatBots.WebexTeams.Mappers.Profiles;
using GlobalX.ChatBots.WebexTeams.Models;
using GlobalX.ChatBots.WebexTeams.Services;
using GlobalX.ChatBots.WebexTeams.Tests.TestData;
using Shouldly;
using TestStack.BDDfy;
using Xunit;
using GlobalXMessage = GlobalX.ChatBots.Core.Messages.Message;
using WebexTeamsMessage = GlobalX.ChatBots.WebexTeams.Models.Message;

namespace GlobalX.ChatBots.WebexTeams.Tests.Services
{
    public class WebexTeamsMessageParserTest
    {
        private CreateMessageRequest _createMessageRequest;
        private WebexTeamsMessage _webexTeamsMessage;
        private GlobalXMessage _globalXMessage;
        private Exception _exception;

        private WebexTeamsMessageParser _subject;

        public WebexTeamsMessageParserTest()
        {
            var mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<CommonMappers>();
                c.AddProfile<MessageMapper>();
            }).CreateMapper();
            _subject = new WebexTeamsMessageParser(new WebexTeamsMapper(mapper));
        }

        [Theory]
        [MemberData(nameof(WebexTeamsMessageParserTestData.ParseMessageTestData), MemberType = typeof(WebexTeamsMessageParserTestData))]
        internal void TestParseMessage(WebexTeamsMessage input, GlobalXMessage output)
        {
            this.Given(x => GivenAWebexTeamsMessage(input))
                .When(x => WhenParsingAMessage())
                .Then(x => ThenItShouldReturnAGlobalXMessage(output))
                .And(x => ThenItShouldNotThrowAnException())
                .BDDfy();
        }

        [Theory]
        [MemberData(nameof(WebexTeamsMessageParserTestData.UnsuccessfulParseMessageTestData), MemberType =
            typeof(WebexTeamsMessageParserTestData))]
        internal void TestUnsuccessfulParseMessage(WebexTeamsMessage input, Type exceptionType, string message)
        {
            this.Given(x => GivenAWebexTeamsMessage(input))
                .When(x => WhenParsingAMessage())
                .Then(x => ThenItShouldThrowAnExceptionOfType(exceptionType))
                .And(x => ThenTheExceptionMessageShouldContain(message))
                .BDDfy();
        }

        [Theory]
        [MemberData(nameof(WebexTeamsMessageParserTestData.ParseCreateMessageRequestTestData), MemberType = typeof(WebexTeamsMessageParserTestData))]
        internal void TestParseCreateMessageRequest(GlobalXMessage input, CreateMessageRequest output)
        {
            this.Given(x => GivenAGlobalXMessage(input))
                .When(x => WhenParsingACreateMessageRequest())
                .Then(x => ThenItShouldReturnACreateMessageRequest(output))
                .And(x => ThenItShouldNotThrowAnException())
                .BDDfy();
        }

        [Theory]
        [MemberData(nameof(WebexTeamsMessageParserTestData.UnsuccessfulParseCreateMessageRequestTestData), MemberType =
            typeof(WebexTeamsMessageParserTestData))]
        internal void TestUnsuccessfulParseCreateMessageRequest(GlobalXMessage input, Type exceptionType, string message)
        {
            this.Given(x => GivenAGlobalXMessage(input))
                .When(x => WhenParsingACreateMessageRequest())
                .Then(x => ThenItShouldThrowAnExceptionOfType(exceptionType))
                .And(x => ThenTheExceptionMessageShouldContain(message))
                .BDDfy();
        }

        private void GivenAWebexTeamsMessage(WebexTeamsMessage message)
        {
            _webexTeamsMessage = message;
        }

        private void GivenAGlobalXMessage(GlobalXMessage message)
        {
            _globalXMessage = message;
        }

        private void WhenParsingAMessage()
        {
            try
            {
                _globalXMessage = _subject.ParseMessage(_webexTeamsMessage);
            }
            catch (Exception e)
            {
                _exception = e;
            }
        }

        private void WhenParsingACreateMessageRequest()
        {
            try
            {
                _createMessageRequest = _subject.ParseCreateMessageRequest(_globalXMessage);
            }
            catch (Exception e)
            {
                _exception = e;
            }
        }

        private void ThenItShouldReturnAGlobalXMessage(GlobalXMessage message)
        {
            _globalXMessage.ShouldNotBeNull();
            _globalXMessage.ShouldSatisfyAllConditions(
                () => _globalXMessage.Created.ShouldBe(message.Created),
                () => _globalXMessage.Text.ShouldBe(message.Text),
                () => {
                    if (message.MessageParts != null)
                    {
                        _globalXMessage.MessageParts.ShouldNotBeNull();
                        _globalXMessage.MessageParts.Length.ShouldBe(message.MessageParts.Length);
                        for (int i = 0; i < _globalXMessage.MessageParts.Length; i++)
                        {
                            _globalXMessage.MessageParts[i].MessageType.ShouldBe(message.MessageParts[i].MessageType);
                            _globalXMessage.MessageParts[i].Text.ShouldBe(message.MessageParts[i].Text);
                            _globalXMessage.MessageParts[i].UserId.ShouldBe(message.MessageParts[i].UserId);
                        }
                    }
                    else
                    {
                        _globalXMessage.MessageParts.ShouldBeNull();
                    }
                },
                () => _globalXMessage.SenderId.ShouldBe(message.SenderId),
                () => _globalXMessage.SenderName.ShouldBe(message.SenderName),
                () => _globalXMessage.RoomId.ShouldBe(message.RoomId),
                () => _globalXMessage.RoomType.ShouldBe(message.RoomType)
            );
        }

        private void ThenItShouldReturnACreateMessageRequest(CreateMessageRequest request)
        {
            _createMessageRequest.ShouldNotBeNull();
            _createMessageRequest.ShouldSatisfyAllConditions(
                () => _createMessageRequest.RoomId.ShouldBe(request.RoomId),
                () => _createMessageRequest.ToPersonId.ShouldBe(request.ToPersonId),
                () => _createMessageRequest.ToPersonEmail.ShouldBe(request.ToPersonEmail),
                () => _createMessageRequest.Text.ShouldBe(request.Text),
                () => _createMessageRequest.Markdown.ShouldBe(request.Markdown),
                () => {
                    if (request.Files != null)
                    {
                        _createMessageRequest.Files.ShouldNotBeNull();
                        _createMessageRequest.Files.OrderBy(x => x).SequenceEqual(request.Files.OrderBy(x => x)).ShouldBeTrue();
                    }
                    else
                    {
                        _createMessageRequest.Files.ShouldBeNull();
                    }
                }
            );
        }

        private void ThenItShouldNotThrowAnException()
        {
            _exception.ShouldBeNull();
        }

        private void ThenItShouldThrowAnExceptionOfType(Type exceptionType)
        {
            _exception.ShouldNotBeNull();
            _exception.ShouldBeOfType(exceptionType);
        }

        private void ThenTheExceptionMessageShouldContain(string message)
        {
            _exception.Message.ShouldContain(message);
        }
    }
}
