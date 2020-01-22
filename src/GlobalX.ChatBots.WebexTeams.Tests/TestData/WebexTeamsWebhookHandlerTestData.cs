using System;
using System.Collections.Generic;
using System.Globalization;
using GlobalX.ChatBots.Core.Messages;
using GlobalX.ChatBots.Core.Rooms;
using GlobalX.ChatBots.WebexTeams.Configuration;
using GlobalX.ChatBots.WebexTeams.Models;
using GlobalXMessage = GlobalX.ChatBots.Core.Messages.Message;
using WebexTeamsMessage = GlobalX.ChatBots.WebexTeams.Models.Message;
using GlobalXPerson = GlobalX.ChatBots.Core.People.Person;
using WebexTeamsPerson = GlobalX.ChatBots.WebexTeams.Models.Person;
using GlobalX.ChatBots.Core.People;

namespace GlobalX.ChatBots.WebexTeams.Tests.TestData
{
    public class WebexTeamsWebhookHandlerTestData
    {
        public static IEnumerable<object[]> SuccessfulRegisterHooksTestData()
        {
            yield return new object[]
            {
                new WebexTeamsSettings
                {
                    WebexTeamsApiUrl = "Test",
                    BotAuthToken = "token 1234",
                    Webhooks = new []
                    {
                        new Configuration.Webhook
                        {
                            Name = "TestBot webhook",
                            TargetUrl = "https://test-bot.test.com/bots/test/respond",
                            Resource = ResourceType.Messages,
                            Event = EventType.Created,
                            Filter = "mentionedPeople=me"
                        },
                        new Configuration.Webhook
                        {
                            Name = "TestBot direct webhook",
                            TargetUrl = "https://test-bot.test.com/bots/test/pm",
                            Resource = ResourceType.Rooms,
                            Event = EventType.Deleted,
                            Filter = "roomType=direct"
                        },
                        new Configuration.Webhook
                        {
                            Name = "TestBot third webhook",
                            TargetUrl = "https://test-bot.test.com/bots/test/memberships",
                            Resource = ResourceType.Memberships,
                            Event = EventType.Updated,
                            Filter = "blah"
                        }
                    }
                },
                new []
                {
                    new Models.Webhook
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
                    new Models.Webhook
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
                        Created = DateTime.Parse("2019-07-15T00:59:51.425Z", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal)
                    }
                },
                new []
                {
                    new CreateWebhookRequest
                    {
                        Name = "TestBot webhook",
                        TargetUrl = "https://test-bot.test.com/bots/test/respond",
                        Resource = "messages",
                        Event = "created",
                        Filter = "mentionedPeople=me"
                    },
                    new CreateWebhookRequest
                    {
                        Name = "TestBot direct webhook",
                        TargetUrl = "https://test-bot.test.com/bots/test/pm",
                        Resource = "rooms",
                        Event = "deleted",
                        Filter = "roomType=direct"
                    },
                    new CreateWebhookRequest
                    {
                        Name = "TestBot third webhook",
                        TargetUrl = "https://test-bot.test.com/bots/test/memberships",
                        Resource = "memberships",
                        Event = "updated",
                        Filter = "blah"
                    }
                }
            };
        }

        public static IEnumerable<object[]> SuccessfulProcessMessageWebhookCallbackTestData()
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
                @"{
	""data"": {
		""id"": ""messageId""
	}
}",
                "messageId",
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
                    RoomId = "roomId",
                    RoomType = RoomType.Group
                }
            };

            yield return new object[]
            {
                @"{
	""data"": {
		""id"": ""directMessageId""
	}
}",
                "directMessageId",
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
    }
}
