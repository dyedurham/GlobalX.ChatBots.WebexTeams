using System.Threading.Tasks;
using AutoMapper;
using GlobalX.ChatBots.WebexTeams.Mappers;
using GlobalX.ChatBots.WebexTeams.Mappers.Profiles;
using GlobalX.ChatBots.WebexTeams.Services;
using GlobalX.ChatBots.WebexTeams.Tests.TestData;
using NSubstitute;
using Shouldly;
using TestStack.BDDfy;
using Xunit;
using GlobalXRoom = GlobalX.ChatBots.Core.Rooms.Room;
using WebexTeamsRoom = GlobalX.ChatBots.WebexTeams.Models.Room;

namespace GlobalX.ChatBots.WebexTeams.Tests.Services
{
    public class WebexTeamsRoomHandlerTest
    {
        private string _input;
        private GlobalXRoom _output;

        private WebexTeamsRoomHandler _subject;

        // mocks
        private readonly IWebexTeamsApiService _apiService;

        public WebexTeamsRoomHandlerTest()
        {
            _apiService = Substitute.For<IWebexTeamsApiService>();
            var mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<CommonMappers>();
                c.AddProfile<RoomMapper>();
            }).CreateMapper();
            _subject = new WebexTeamsRoomHandler(_apiService, new WebexTeamsMapper(mapper));
        }

        [Theory]
        [MemberData(nameof(WebexTeamsRoomHandlerTestData.SuccessfulGetRoomTestData), MemberType = typeof(WebexTeamsRoomHandlerTestData))]
        internal void TestGetPerson(string input, WebexTeamsRoom apiResponse, GlobalXRoom output)
        {
            this.Given(x => GivenAPersonId(input))
                .When(x => WhenGettingAPerson(apiResponse))
                .Then(x => ThenItShouldReturnPersonDetails(output))
                .BDDfy();
        }

        private void GivenAPersonId(string input)
        {
            _input = input;
        }

        private async void WhenGettingAPerson(WebexTeamsRoom apiResponse)
        {
            _apiService.GetRoomAsync(_input).Returns(Task.FromResult(apiResponse));
            _output = await _subject.GetRoomAsync(_input);
        }

        private void ThenItShouldReturnPersonDetails(GlobalXRoom output)
        {
            _output.ShouldNotBeNull();
            _output.ShouldSatisfyAllConditions(
                () => _output.Created.ShouldBe(output.Created),
                () => _output.Id.ShouldBe(output.Id),
                () => _output.Title.ShouldBe(output.Title),
                () => _output.Type.ShouldBe(output.Type)
            );
        }
    }
}
