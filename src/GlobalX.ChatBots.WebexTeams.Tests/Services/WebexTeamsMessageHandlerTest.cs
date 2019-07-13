using System.Threading.Tasks;
using GlobalX.ChatBots.WebexTeams.Models;
using GlobalX.ChatBots.WebexTeams.Services;
using GlobalX.ChatBots.WebexTeams.Tests.TestData;
using NSubstitute;
using Shouldly;
using TestStack.BDDfy;
using Xunit;
using GlobalXMessage = GlobalX.ChatBots.Core.Messages.Message;
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
            _subject = new WebexTeamsMessageHandler(_apiService, _messageParser);
        }

        [Theory]
        [MemberData(nameof(WebexTeamsMessageHandlerTestData.SuccessfulSendMessageTestData), MemberType = typeof(WebexTeamsMessageHandlerTestData))]
        public void TestSendMessage(GlobalXMessage input, CreateMessageRequest parsedInput, WebexTeamsMessage apiResponse, GlobalXMessage output)
        {
            this.Given(x => GivenAGlobalXMessage(input))
                .When(x => WhenSendingAMessage(parsedInput, apiResponse, output))
                .Then(x => ThenItShouldCallApiService())
                .And(x => ThenItShouldReturnAMessage(output))
                .BDDfy();
        }

        private void GivenAGlobalXMessage(GlobalXMessage input)
        {
            _input = input;
        }

        private async void WhenSendingAMessage(CreateMessageRequest parsedInput, WebexTeamsMessage apiResponse, GlobalXMessage output)
        {
            _messageParser.ParseCreateMessageRequest(_input).Returns(parsedInput);
            _apiService.SendMessageAsync(parsedInput).Returns(Task.FromResult(apiResponse));
            _messageParser.ParseMessage(apiResponse).Returns(output);
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
                () => _output.SenderId.ShouldBe(output.SenderId),
                () => _output.SenderName.ShouldBe(output.SenderName),
                () => _output.RoomId.ShouldBe(output.RoomId),
                () => _output.RoomType.ShouldBe(output.RoomType)
            );
        }
    }
}
