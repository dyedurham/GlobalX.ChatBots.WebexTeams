using System;
using System.Collections.Generic;
using System.Globalization;
using GlobalX.ChatBots.Core.People;
using GlobalX.ChatBots.WebexTeams.Models;
using GlobalXPerson = GlobalX.ChatBots.Core.People.Person;
using WebexTeamsPerson = GlobalX.ChatBots.WebexTeams.Models.Person;

namespace GlobalX.ChatBots.WebexTeams.Tests.TestData
{
    public class WebexTeamsPersonHandlerTestData
    {
        public static IEnumerable<object[]> SuccessfulGetPersonTestData()
        {
            yield return new object[]
            {
                "personId",
                new WebexTeamsPerson
                {
                    Id = "personId",
                    Emails = new []{ "test.person@test.com" },
                    DisplayName = "Test Person",
                    NickName = "Testy",
                    FirstName = "Test",
                    LastName = "Person",
                    Avatar = "https://fake-address.com",
                    OrgId = "orgId",
                    Created = DateTime.Parse("2018-08-12T22:17:24.697Z", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal),
                    Status = "unknown",
                    Type = "person"
                },
                new GlobalXPerson
                {
                    Created = DateTime.Parse("2018-08-12T22:17:24.697Z", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal),
                    Username = "Test Person",
                    UserId = "personId",
                    Emails = new []{ "test.person@test.com" },
                    Type = PersonType.Person
                }
            };

            yield return new object[]
            {
                "botId",
                new WebexTeamsPerson
                {
                    Id = "botId",
                    Emails = new []{ "TestBot@webex.bot" },
                    PhoneNumbers = new PhoneNumber[0],
                    DisplayName = "TestBot",
                    NickName = "TestBot",
                    Avatar = "https://test-bot-avatar.com",
                    OrgId = "orgId",
                    Created = DateTime.Parse("2018-12-17T22:26:10.283Z", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal),
                    Status = "unknown",
                    Type = "bot"
                },
                new GlobalXPerson
                {
                    Created = DateTime.Parse("2018-12-17T22:26:10.283Z", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal),
                    Username = "TestBot",
                    UserId = "botId",
                    Emails = new []{ "TestBot@webex.bot" },
                    Type = PersonType.Bot
                }
            };
        }
    }
}
