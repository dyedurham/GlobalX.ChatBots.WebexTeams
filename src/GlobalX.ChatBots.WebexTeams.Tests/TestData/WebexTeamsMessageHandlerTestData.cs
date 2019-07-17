using System;
using System.Collections.Generic;
using System.Globalization;
using GlobalX.ChatBots.Core.Messages;
using GlobalX.ChatBots.Core.Rooms;
using GlobalX.ChatBots.WebexTeams.Models;
using GlobalXMessage = GlobalX.ChatBots.Core.Messages.Message;
using WebexTeamsMessage = GlobalX.ChatBots.WebexTeams.Models.Message;

namespace GlobalX.ChatBots.WebexTeams.Tests.TestData
{
    public class WebexTeamsMessageHandlerTestData
    {
        public static IEnumerable<object[]> SuccessfulSendMessageTestData()
        {
            yield return new object[]
            {
                new GlobalXMessage
                {
                    Text = "TestBot All test things",
                    MessageParts = new[]
                    {
                        new MessagePart
                        {
                            MessageType = MessageType.PersonMention,
                            Text = "TestBot",
                            UserId = "testBotId"
                        },
                        new MessagePart
                        {
                            MessageType = MessageType.Text,
                            Text = " "
                        },
                        new MessagePart
                        {
                            MessageType = MessageType.GroupMention,
                            Text = "All",
                            UserId = "all"
                        },
                        new MessagePart
                        {
                            MessageType = MessageType.Text,
                            Text = " test things",
                        }
                    },
                    RoomId = "Y2lzY29zcGFyazovL3VzL1JPT00vcm9vbUlk",
                },
                new CreateMessageRequest
                {
                    RoomId = "Y2lzY29zcGFyazovL3VzL1JPT00vcm9vbUlk",
                    Markdown = "<@personId:testBotId|TestBot> <@all> test things"
                },
                new WebexTeamsMessage
                {
                    Id = "messageId",
                    RoomId = "Y2lzY29zcGFyazovL3VzL1JPT00vcm9vbUlk",
                    RoomType = "group",
                    Text = "Person All test things",
                    PersonId = "testBotId",
                    PersonEmail = "test.bot@test.com",
                    Html = "<p><spark-mention data-object-type=\"person\" data-object-id=\"personId\">Person</spark-mention> <spark-mention data-object-type=\"groupMention\" data-group-type=\"all\">All</spark-mention> test things</p>",
                    MentionedPeople = new[]{ "personId" },
                    MentionedGroups = new []{ "all" },
                    Created = DateTime.Parse("2019-07-09T22:55:52.357Z", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal)
                },
                new GlobalXMessage
                {
                    Created = new DateTime(2019, 7, 8, 22, 55, 52),
                    Text = "TestBot All test things",
                    MessageParts = new[]
                    {
                        new MessagePart
                        {
                            MessageType = MessageType.PersonMention,
                            Text = "TestBot",
                            UserId = "testBotId"
                        },
                        new MessagePart
                        {
                            MessageType = MessageType.Text,
                            Text = " "
                        },
                        new MessagePart
                        {
                            MessageType = MessageType.GroupMention,
                            Text = "All",
                            UserId = "all"
                        },
                        new MessagePart
                        {
                            MessageType = MessageType.Text,
                            Text = " test things",
                        }
                    },
                    SenderId = "senderId",
                    RoomId = "Y2lzY29zcGFyazovL3VzL1JPT00vcm9vbUlk",
                    RoomType = RoomType.Group
                },
                new Person
                {
                    Id = "senderId",
                    DisplayName = "SenderName"
                },
                new GlobalXMessage
                {
                    Created = new DateTime(2019, 7, 8, 22, 55, 52),
                    Text = "TestBot All test things",
                    MessageParts = new[]
                    {
                        new MessagePart
                        {
                            MessageType = MessageType.PersonMention,
                            Text = "TestBot",
                            UserId = "testBotId"
                        },
                        new MessagePart
                        {
                            MessageType = MessageType.Text,
                            Text = " "
                        },
                        new MessagePart
                        {
                            MessageType = MessageType.GroupMention,
                            Text = "All",
                            UserId = "all"
                        },
                        new MessagePart
                        {
                            MessageType = MessageType.Text,
                            Text = " test things",
                        }
                    },
                    SenderId = "senderId",
                    SenderName = "SenderName",
                    RoomId = "Y2lzY29zcGFyazovL3VzL1JPT00vcm9vbUlk",
                    RoomType = RoomType.Group
                }
            };

            yield return new object[]
            {
                new GlobalXMessage
                {
                    Text = "test",
                    RoomId = "Y2lzY29zcGFyazovL3VzL1BFT1BMRS9wZXJzb25JZA==",
                    RoomType = RoomType.Direct
                },
                new CreateMessageRequest
                {
                    ToPersonId = "Y2lzY29zcGFyazovL3VzL1BFT1BMRS9wZXJzb25JZA==",
                    Markdown = "test"
                },
                new WebexTeamsMessage
                {
                    Id = "directMessageId",
                    RoomId = "roomId",
                    RoomType = "direct",
                    Text = "test",
                    PersonId = "testBotId",
                    PersonEmail = "test.bot@test.com",
                    Created = DateTime.Parse("2019-06-30T22:32:59.050Z", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal)
                },
                new GlobalXMessage
                {
                    Created = new DateTime(2019, 6, 30, 22, 32, 59),
                    Text = "test",
                    MessageParts = new[]
                    {
                        new MessagePart
                        {
                            MessageType = MessageType.Text,
                            Text = "test"
                        }
                    },
                    SenderId = "senderId",
                    RoomId = "roomId",
                    RoomType = RoomType.Direct
                },
                new Person
                {
                    Id = "senderId",
                    DisplayName = "SenderName"
                },
                new GlobalXMessage
                {
                    Created = new DateTime(2019, 6, 30, 22, 32, 59),
                    Text = "test",
                    MessageParts = new[]
                    {
                        new MessagePart
                        {
                            MessageType = MessageType.Text,
                            Text = "test"
                        }
                    },
                    SenderId = "senderId",
                    SenderName = "SenderName",
                    RoomId = "roomId",
                    RoomType = RoomType.Direct
                }
            };
        }
    }
}