using System;
using System.Collections.Generic;
using System.Xml;
using GlobalX.ChatBots.Core.Messages;
using GlobalX.ChatBots.Core.Rooms;
using GlobalX.ChatBots.WebexTeams.Models;
using GlobalXMessage = GlobalX.ChatBots.Core.Messages.Message;
using WebexTeamsMessage = GlobalX.ChatBots.WebexTeams.Models.Message;

namespace GlobalX.ChatBots.WebexTeams.Tests.TestData
{
    public class WebexTeamsMessageParserTestData
    {
        public static IEnumerable<object[]> ParseMessageTestData()
        {
            yield return new object[]
            {
                new WebexTeamsMessage
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
                    Created = new DateTime(2019, 7, 8, 22, 55, 52)
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
                    RoomId = "roomId",
                    RoomType = RoomType.Group
                }
            };

            yield return new object[]
            {
                new WebexTeamsMessage
                {
                    Id = "directMessageId",
                    RoomId = "roomId",
                    RoomType = "direct",
                    Text = "test",
                    PersonId = "senderId",
                    PersonEmail = "sender.email@test.com",
                    Created = new DateTime(2019, 6, 30, 22, 32, 59)
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
                }
            };

            // No root tag
            yield return new object[]
            {
                new WebexTeamsMessage
                {
                    Id = "messageId",
                    RoomId = "roomId",
                    RoomType = "group",
                    Text = "TestBot All test things",
                    PersonId = "senderId",
                    PersonEmail = "sender.email@test.com",
                    Html = "<spark-mention data-object-type=\"person\" data-object-id=\"testBotId\">TestBot</spark-mention> <spark-mention data-object-type=\"groupMention\" data-group-type=\"all\">All</spark-mention> test things",
                    MentionedPeople = new[]{ "testBotId" },
                    MentionedGroups = new []{ "all" },
                    Created = new DateTime(2019, 7, 8, 22, 55, 52)
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
                    RoomId = "roomId",
                    RoomType = RoomType.Group
                }
            };

            // Parse iOS user ID
            yield return new object[]
            {
                new WebexTeamsMessage
                {
                    Id = "messageId",
                    RoomId = "roomId",
                    RoomType = "group",
                    Text = "TestBot All test things",
                    PersonId = "senderId",
                    PersonEmail = "sender.email@test.com",
                    Html = "<spark-mention data-object-type=\"person\" data-object-id=\"1-2-3\">TestBot</spark-mention> <spark-mention data-object-type=\"groupMention\" data-group-type=\"all\">All</spark-mention> test things",
                    MentionedPeople = new[]{ "testBotId" },
                    MentionedGroups = new []{ "all" },
                    Created = new DateTime(2019, 7, 8, 22, 55, 52)
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
                            UserId = "Y2lzY29zcGFyazovL3VzL1BFT1BMRS8xLTItMw=="
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
                    RoomId = "roomId",
                    RoomType = RoomType.Group
                }
            };

            // trailing <br/> tags
            yield return new object[]
            {
                new WebexTeamsMessage
                {
                    Id = "messageId",
                    RoomId = "roomId",
                    RoomType = "group",
                    Text = "TestBot All test things",
                    PersonId = "senderId",
                    PersonEmail = "sender.email@test.com",
                    Html = "<p><spark-mention data-object-type=\"person\" data-object-id=\"testBotId\">TestBot</spark-mention> <spark-mention data-object-type=\"groupMention\" data-group-type=\"all\">All</spark-mention> test things</p><br/><br /><br>",
                    MentionedPeople = new[]{ "testBotId" },
                    MentionedGroups = new []{ "all" },
                    Created = new DateTime(2019, 7, 8, 22, 55, 52)
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
                    RoomId = "roomId",
                    RoomType = RoomType.Group
                }
            };
        }

        public static IEnumerable<object[]> UnsuccessfulParseMessageTestData()
        {
            // Invalid html
            yield return new object[]
            {
                new WebexTeamsMessage
                {
                    Id = "messageId",
                    RoomId = "roomId",
                    RoomType = "group",
                    Text = "TestBot All test things",
                    PersonId = "senderId",
                    PersonEmail = "sender.email@test.com",
                    Html = "Invalid<p>",
                    MentionedPeople = new[]{ "testBotId" },
                    MentionedGroups = new []{ "all" },
                    Created = new DateTime(2019, 7, 8, 22, 55, 52)
                },
                typeof(XmlException),
                string.Empty
            };

            // Too many levels
            yield return new object[]
            {
                new WebexTeamsMessage
                {
                    Id = "messageId",
                    RoomId = "roomId",
                    RoomType = "group",
                    Text = "TestBot All test things",
                    PersonId = "senderId",
                    PersonEmail = "sender.email@test.com",
                    Html = "<p><spark-mention data-object-type=\"person\" data-object-id=\"testBotId\">Test<b>Bot</b></spark-mention> <spark-mention data-object-type=\"groupMention\" data-group-type=\"all\">All</spark-mention> test things</p>",
                    MentionedPeople = new[]{ "testBotId" },
                    MentionedGroups = new []{ "all" },
                    Created = new DateTime(2019, 7, 8, 22, 55, 52)
                },
                typeof(ArgumentException),
                "Html has more levels than expected"
            };

            // Mention without user ID
            yield return new object[]
            {
                new WebexTeamsMessage
                {
                    Id = "messageId",
                    RoomId = "roomId",
                    RoomType = "group",
                    Text = "TestBot All test things",
                    PersonId = "senderId",
                    PersonEmail = "sender.email@test.com",
                    Html = "<p><spark-mention data-object-type=\"person\">TestBot</spark-mention> <spark-mention data-object-type=\"groupMention\" data-group-type=\"all\">All</spark-mention> test things</p>",
                    MentionedPeople = new[]{ "testBotId" },
                    MentionedGroups = new []{ "all" },
                    Created = new DateTime(2019, 7, 8, 22, 55, 52)
                },
                typeof(ArgumentException),
                "Mention has no associated user id"
            };

            // Mention with invalid data-object-type
            yield return new object[]
            {
                new WebexTeamsMessage
                {
                    Id = "messageId",
                    RoomId = "roomId",
                    RoomType = "group",
                    Text = "TestBot All test things",
                    PersonId = "senderId",
                    PersonEmail = "sender.email@test.com",
                    Html = "<p><spark-mention data-object-type=\"invalid\">TestBot</spark-mention> <spark-mention data-object-type=\"groupMention\" data-group-type=\"all\">All</spark-mention> test things</p>",
                    MentionedPeople = new[]{ "testBotId" },
                    MentionedGroups = new []{ "all" },
                    Created = new DateTime(2019, 7, 8, 22, 55, 52)
                },
                typeof(ArgumentException),
                "Invalid mention type invalid"
            };
        }

        public static IEnumerable<object[]> ParseCreateMessageRequestTestData()
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
                }
            };

            // ID not padded to multiple of 4 characters
            yield return new object[]
            {
                new GlobalXMessage
                {
                    Text = "test",
                    RoomId = "Y2lzY29zcGFyazovL3VzL1BFT1BMRS9wZXJzb25JZA",
                    RoomType = RoomType.Direct
                },
                new CreateMessageRequest
                {
                    ToPersonId = "Y2lzY29zcGFyazovL3VzL1BFT1BMRS9wZXJzb25JZA",
                    Markdown = "test"
                }
            };
        }

        public static IEnumerable<object[]> UnsuccessfulParseCreateMessageRequestTestData()
        {
            // Invalid room ID (not base64)
            yield return new object[]
            {
                new GlobalXMessage
                {
                    Text = "test",
                    RoomId = "invalid",
                    RoomType = RoomType.Direct
                },
                typeof(ArgumentException),
                "Invalid room ID invalid"
            };

            //Invalid room ID (incorrect prefix)
            yield return new object[]
            {
                new GlobalXMessage
                {
                    Text = "test",
                    RoomId = "aW52YWxpZA==",
                    RoomType = RoomType.Direct
                },
                typeof(ArgumentException),
                "Invalid room ID aW52YWxpZA=="
            };

            // No message
            yield return new object[]
            {
                new GlobalXMessage
                {
                    RoomId = "Y2lzY29zcGFyazovL3VzL1JPT00vcm9vbUlk",
                    RoomType = RoomType.Direct
                },
                typeof(ArgumentException),
                "Please provide a message to send"
            };

            // Invalid message type
            yield return new object[]
            {
                new GlobalXMessage
                {
                    Text = "TestBot All test things",
                    MessageParts = new[]
                    {
                        new MessagePart
                        {
                            MessageType = (MessageType)1234,
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
                    RoomType = RoomType.Direct
                },
                typeof(ArgumentException),
                "Invalid message type"
            };
        }
    }
}
