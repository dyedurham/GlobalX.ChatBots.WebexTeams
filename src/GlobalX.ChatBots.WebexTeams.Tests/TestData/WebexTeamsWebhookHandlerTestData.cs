using System;
using System.Collections.Generic;
using System.Globalization;
using GlobalX.ChatBots.WebexTeams.Configuration;
using GlobalX.ChatBots.WebexTeams.Models;

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
    }
}
