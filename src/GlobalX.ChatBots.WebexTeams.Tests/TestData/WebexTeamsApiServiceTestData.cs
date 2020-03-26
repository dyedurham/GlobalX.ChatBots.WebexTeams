using System;
using System.Collections.Generic;
using System.Globalization;
using GlobalX.ChatBots.WebexTeams.Models;

namespace GlobalX.ChatBots.WebexTeams.Tests.TestData
{
    public class WebexTeamsApiServiceTestData
    {
        public static IEnumerable<object[]> GetMessageTestData()
        {
            yield return new object[]
            {
                "messageId",
                @"{
    ""id"": ""messageId"",
    ""roomId"": ""roomId"",
    ""roomType"": ""group"",
    ""text"": ""TestBot All test things"",
    ""personId"": ""senderId"",
    ""personEmail"": ""sender.email@test.com"",
    ""html"": ""<p><spark-mention data-object-type=\""person\"" data-object-id=\""testBotId\"">TestBot</spark-mention> <spark-mention data-object-type=\""groupMention\"" data-group-type=\""all\"">All</spark-mention> test things</p>"",
    ""mentionedPeople"": [
        ""testBotId""
    ],
    ""mentionedGroups"": [
        ""all""
    ],
    ""created"": ""2019-07-08T22:55:52.357Z""
}",
                new Message
                {
                    Id = "messageId",
                    RoomId = "roomId",
                    RoomType = "group",
                    Text = "TestBot All test things",
                    PersonId = "senderId",
                    PersonEmail = "sender.email@test.com",
                    Html = "<p><spark-mention data-object-type=\"person\" data-object-id=\"testBotId\">TestBot</spark-mention> <spark-mention data-object-type=\"groupMention\" data-group-type=\"all\">All</spark-mention> test things</p>",
                    MentionedPeople = new[]{ "testBotId" },
                    MentionedGroups = new []{ "all" },
                    Created = DateTime.Parse("2019-07-08T22:55:52.357Z", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal)
                }
            };

            yield return new object[]
            {
                "directMessageId",
                @"{
    ""id"": ""directMessageId"",
    ""roomId"": ""roomId"",
    ""roomType"": ""direct"",
    ""text"": ""test"",
    ""personId"": ""senderId"",
    ""personEmail"": ""sender.email@test.com"",
    ""created"": ""2019-06-30T22:32:59.050Z""
}",
                new Message
                {
                    Id = "directMessageId",
                    RoomId = "roomId",
                    RoomType = "direct",
                    Text = "test",
                    PersonId = "senderId",
                    PersonEmail = "sender.email@test.com",
                    Created = DateTime.Parse("2019-06-30T22:32:59.050Z", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal)
                }
            };
        }

        public static IEnumerable<object[]> SendMessageTestData()
        {
            yield return new object[]
            {
                new CreateMessageRequest
                {
                    RoomId = "roomId",
                    Markdown = "<@personId|Person> <@all> test things"
                },
                @"{
    ""id"": ""messageId"",
    ""roomId"": ""roomId"",
    ""roomType"": ""group"",
    ""text"": ""Person All test things"",
    ""personId"": ""testBotId"",
    ""personEmail"": ""test.bot@test.com"",
    ""html"": ""<p><spark-mention data-object-type=\""person\"" data-object-id=\""personId\"">Person</spark-mention> <spark-mention data-object-type=\""groupMention\"" data-group-type=\""all\"">All</spark-mention> test things</p>"",
    ""mentionedPeople"": [
        ""personId""
    ],
    ""mentionedGroups"": [
        ""all""
    ],
    ""created"": ""2019-07-09T22:55:52.357Z""
}",
                new Message
                {
                    Id = "messageId",
                    RoomId = "roomId",
                    RoomType = "group",
                    Text = "Person All test things",
                    PersonId = "testBotId",
                    PersonEmail = "test.bot@test.com",
                    Html = "<p><spark-mention data-object-type=\"person\" data-object-id=\"personId\">Person</spark-mention> <spark-mention data-object-type=\"groupMention\" data-group-type=\"all\">All</spark-mention> test things</p>",
                    MentionedPeople = new[]{ "personId" },
                    MentionedGroups = new []{ "all" },
                    Created = DateTime.Parse("2019-07-09T22:55:52.357Z", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal)
                }
            };

            yield return new object[]
            {
                new CreateMessageRequest
                {
                    ToPersonId = "personId",
                    Markdown = "test"
                },
                @"{
    ""id"": ""directMessageId"",
    ""roomId"": ""roomId"",
    ""roomType"": ""direct"",
    ""text"": ""test"",
    ""personId"": ""testBotId"",
    ""personEmail"": ""test.bot@test.com"",
    ""created"": ""2019-06-30T22:32:59.050Z""
}",
                new Message
                {
                    Id = "directMessageId",
                    RoomId = "roomId",
                    RoomType = "direct",
                    Text = "test",
                    PersonId = "testBotId",
                    PersonEmail = "test.bot@test.com",
                    Created = DateTime.Parse("2019-06-30T22:32:59.050Z", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal)
                }
            };
        }

        public static IEnumerable<object[]> GetPersonTestData()
        {
            yield return new object[]
            {
                "personId",
                @"{
    ""id"": ""personId"",
    ""emails"": [
        ""test.person@test.com""
    ],
    ""phoneNumbers"": [
        {
        ""type"": ""work"",
        ""value"": ""+0012345678""
        }
    ],
    ""displayName"": ""Test Person"",
    ""nickName"": ""Testy"",
    ""firstName"": ""Test"",
    ""lastName"": ""Person"",
    ""avatar"": ""https://fake-address.com"",
    ""orgId"": ""orgId"",
    ""created"": ""2018-08-12T22:17:24.697Z"",
    ""status"": ""unknown"",
    ""type"": ""person""
}",
                new Person
                {
                    Id = "personId",
                    Emails = new []{ "test.person@test.com" },
                    PhoneNumbers = new []{ new PhoneNumber { Type = "work", Value = "+0012345678"} },
                    DisplayName = "Test Person",
                    NickName = "Testy",
                    FirstName = "Test",
                    LastName = "Person",
                    Avatar = "https://fake-address.com",
                    OrgId = "orgId",
                    Created = DateTime.Parse("2018-08-12T22:17:24.697Z", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal),
                    Status = "unknown",
                    Type = "person"
                }
            };

            yield return new object[]
            {
                "botId",
                @"{
    ""id"": ""botId"",
    ""emails"": [
        ""TestBot@webex.bot""
    ],
    ""phoneNumbers"": [],
    ""displayName"": ""TestBot"",
    ""nickName"": ""TestBot"",
    ""avatar"": ""https://test-bot-avatar.com"",
    ""orgId"": ""orgId"",
    ""created"": ""2018-12-17T22:26:10.283Z"",
    ""status"": ""unknown"",
    ""type"": ""bot""
}",
                new Person
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
                }
            };
        }

        public static IEnumerable<object[]> GetRoomTestData()
        {
            yield return new object[]
            {
                "roomId",
                @"{
    ""id"": ""roomId"",
    ""title"": ""Awesome Space"",
    ""type"": ""group"",
    ""isLocked"": false,
    ""teamId"": ""teamId"",
    ""lastActivity"": ""2019-07-08T22:55:52.357Z"",
    ""creatorId"": ""creatorId"",
    ""created"": ""2019-06-30T22:32:59.050Z"",
    ""sipAddress"": ""sipAddress""
}",
                new Room
                {
                    Id = "roomId",
                    Title = "Awesome Space",
                    Type = "group",
                    IsLocked = false,
                    TeamId = "teamId",
                    LastActivity = DateTime.Parse("2019-07-08T22:55:52.357Z", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal),
                    CreatorId = "creatorId",
                    Created = DateTime.Parse("2019-06-30T22:32:59.050Z", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal),
                    SipAddress = "sipAddress"
                }
            };
        }

        public static IEnumerable<object[]> GetWebhooksTestData()
        {
            yield return new object[]
            {
                @"{
    ""items"": [
        {
            ""id"": ""webhookId"",
            ""name"": ""TestBot webhook"",
            ""targetUrl"": ""https://test-bot.test.com/bots/test/respond"",
            ""resource"": ""messages"",
            ""event"": ""created"",
            ""filter"": ""mentionedPeople=testBotId"",
            ""orgId"": ""orgId"",
            ""createdBy"": ""testBotId"",
            ""appId"": ""appId"",
            ""ownedBy"": ""creator"",
            ""status"": ""active"",
            ""created"": ""2019-07-15T00:59:51.361Z""
        },
        {
            ""id"": ""directWebhookId"",
            ""name"": ""TestBot direct webhook"",
            ""targetUrl"": ""https://test-bot.test.com/bots/test/pm"",
            ""resource"": ""messages"",
            ""event"": ""created"",
            ""filter"": ""roomType=direct"",
            ""orgId"": ""orgId"",
            ""createdBy"": ""testBotId"",
            ""appId"": ""appId"",
            ""ownedBy"": ""creator"",
            ""status"": ""active"",
            ""secret"": ""secret"",
            ""created"": ""2019-07-15T00:59:51.425Z""
        }
    ]
}",
                new []
                {
                    new Webhook
                    {
                        Id = "webhookId",
                        Name = "TestBot webhook",
                        TargetUrl = "https://test-bot.test.com/bots/test/respond",
                        Resource = "messages",
                        Event = "created",
                        Filter = "mentionedPeople=testBotId",
                        OrgId = "orgId",
                        CreatedBy = "testBotId",
                        AppId = "appId",
                        OwnedBy = "creator",
                        Status = "active",
                        Created = DateTime.Parse("2019-07-15T00:59:51.361Z", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal)
                    },
                    new Webhook
                    {
                        Id = "directWebhookId",
                        Name = "TestBot direct webhook",
                        TargetUrl = "https://test-bot.test.com/bots/test/pm",
                        Resource = "messages",
                        Event = "created",
                        Filter = "roomType=direct",
                        OrgId = "orgId",
                        CreatedBy = "testBotId",
                        AppId = "appId",
                        OwnedBy = "creator",
                        Status = "active",
                        Secret = "secret",
                        Created = DateTime.Parse("2019-07-15T00:59:51.425Z", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal)
                    }
                }
            };
        }

        public static IEnumerable<object[]> CreateWebhookTestData()
        {
            yield return new object[]
            {
                new CreateWebhookRequest
                {
                    Name = "TestBot webhook",
                    TargetUrl = "https://test-bot.test.com/bots/test/respond",
                    Resource = "messages",
                    Event = "created",
                    Filter = "mentionedPeople=me",
                    Secret = "secret"
                },
                @"{
    ""id"": ""webhookId"",
    ""name"": ""TestBot webhook"",
    ""targetUrl"": ""https://test-bot.test.com/bots/test/respond"",
    ""resource"": ""messages"",
    ""event"": ""created"",
    ""filter"": ""mentionedPeople=testBotId"",
    ""orgId"": ""orgId"",
    ""createdBy"": ""testBotId"",
    ""appId"": ""appId"",
    ""ownedBy"": ""creator"",
    ""status"": ""active"",
    ""secret"": ""secret"",
    ""created"": ""2019-07-15T00:59:51.361Z""
}",
                new Webhook
                {
                    Id = "webhookId",
                    Name = "TestBot webhook",
                    TargetUrl = "https://test-bot.test.com/bots/test/respond",
                    Resource = "messages",
                    Event = "created",
                    Filter = "mentionedPeople=testBotId",
                    OrgId = "orgId",
                    CreatedBy = "testBotId",
                    AppId = "appId",
                    OwnedBy = "creator",
                    Status = "active",
                    Secret = "secret",
                    Created = DateTime.Parse("2019-07-15T00:59:51.361Z", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal)
                }
            };
        }

        public static IEnumerable<object[]> DeleteWebhookTestData()
        {
            return new List<object[]>
            {
                new object[] { "webhookId" },
                new object[] { "anotherId" }
            };
        }
    }
}
