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
    }
}
