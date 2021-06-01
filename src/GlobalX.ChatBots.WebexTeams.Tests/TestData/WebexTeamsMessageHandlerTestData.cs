using System;
using System.Collections.Generic;
using System.Globalization;
using GlobalX.ChatBots.Core.Messages;
using GlobalX.ChatBots.Core.People;
using GlobalX.ChatBots.Core.Rooms;
using GlobalX.ChatBots.WebexTeams.Models;
using GlobalXMessage = GlobalX.ChatBots.Core.Messages.Message;
using WebexTeamsMessage = GlobalX.ChatBots.WebexTeams.Models.Message;
using GlobalXPerson = GlobalX.ChatBots.Core.People.Person;
using WebexTeamsPerson = GlobalX.ChatBots.WebexTeams.Models.Person;

namespace GlobalX.ChatBots.WebexTeams.Tests.TestData
{
    public class WebexTeamsMessageHandlerTestData
    {
        public static IEnumerable<object[]> SuccessfulSendMessageTestData()
        {
            var sender = new WebexTeamsPerson
            {
                Id = "senderId",
                DisplayName = "SenderName",
                Type = "person"
            };
            var mappedSender = new GlobalXPerson
            {
                Type = PersonType.Person,
                UserId = "senderId",
                Username = "SenderName",
                Emails = Array.Empty<string>()
            };

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
                    RoomId = "Y2lzY29zcGFyazovL3VzL1JPT00vcm9vbUlk",
                    RoomType = RoomType.Group
                },
                sender,
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
                    Sender = mappedSender,
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
                    PersonId = "senderId",
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
                    RoomId = "roomId",
                    RoomType = RoomType.Direct
                },
                sender,
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
                    Sender = mappedSender,
                    RoomId = "roomId",
                    RoomType = RoomType.Direct
                }
            };
        }

        public static IEnumerable<object[]> BadParentMessageTestData()
        {
            yield return new object[]
            {
                new GlobalXMessage
                {
                    ParentId = "badParentId",
                    Text = "test message"
                },
                new WebexTeamsMessage
                {
                    Text = "test message",
                    ParentId = "goodParentId",
                    PersonId = "personId"
                }
            };
        }
    }
}
