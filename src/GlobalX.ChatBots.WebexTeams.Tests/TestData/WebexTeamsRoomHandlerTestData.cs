using System;
using System.Collections.Generic;
using System.Globalization;
using GlobalX.ChatBots.Core.Rooms;
using GlobalXRoom = GlobalX.ChatBots.Core.Rooms.Room;
using WebexTeamsRoom = GlobalX.ChatBots.WebexTeams.Models.Room;

namespace GlobalX.ChatBots.WebexTeams.Tests.TestData
{
    public class WebexTeamsRoomHandlerTestData
    {
        public static IEnumerable<object[]> SuccessfulGetRoomTestData()
        {
            yield return new object[]
            {
                "roomId",
                new WebexTeamsRoom
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
                },
                new GlobalXRoom
                {
                    Created = DateTime.Parse("2019-06-30T22:32:59.050Z", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal),
                    Id = "roomId",
                    Title = "Awesome Space",
                    Type = RoomType.Group
                }
            };
        }
    }
}
