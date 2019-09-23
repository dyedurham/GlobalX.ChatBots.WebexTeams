using System.Linq;
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
using GlobalXPerson = GlobalX.ChatBots.Core.People.Person;
using WebexTeamsPerson = GlobalX.ChatBots.WebexTeams.Models.Person;

namespace GlobalX.ChatBots.WebexTeams.Tests.Services
{
    public class WebexTeamsPersonHandlerTest
    {
        private string _input;
        private GlobalXPerson _output;

        private WebexTeamsPersonHandler _subject;

        // mocks
        private readonly IWebexTeamsApiService _apiService;

        public WebexTeamsPersonHandlerTest()
        {
            _apiService = Substitute.For<IWebexTeamsApiService>();
            var mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<CommonMappers>();
                c.AddProfile<PersonMapper>();
            }).CreateMapper();
            _subject = new WebexTeamsPersonHandler(_apiService, new WebexTeamsMapper(mapper));
        }

        [Theory]
        [MemberData(nameof(WebexTeamsPersonHandlerTestData.SuccessfulGetPersonTestData), MemberType =
            typeof(WebexTeamsPersonHandlerTestData))]
        internal void TestGetPerson(string input, WebexTeamsPerson apiResponse, GlobalXPerson output)
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

        private async void WhenGettingAPerson(WebexTeamsPerson apiResponse)
        {
            _apiService.GetPersonAsync(_input).Returns(Task.FromResult(apiResponse));
            _output = await _subject.GetPersonAsync(_input);
        }

        private void ThenItShouldReturnPersonDetails(GlobalXPerson output)
        {
            _output.ShouldNotBeNull();
            _output.ShouldSatisfyAllConditions(
                () => _output.Created.ShouldBe(output.Created),
                () => _output.Username.ShouldBe(output.Username),
                () => _output.UserId.ShouldBe(output.UserId),
                () =>
                {
                    if (output.Emails != null)
                    {
                        _output.Emails.ShouldNotBeNull();
                        _output.Emails.OrderBy(x => x).SequenceEqual(output.Emails.OrderBy(x => x)).ShouldBe(true);
                    }
                    else
                    {
                        _output.Emails.ShouldBeNull();
                    }
                },
                () => _output.Type.ShouldBe(output.Type)
            );
        }
    }
}
