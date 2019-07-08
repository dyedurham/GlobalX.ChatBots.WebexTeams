using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GlobalX.ChatBots.Core.Rooms;

namespace GlobalX.ChatBots.WebexTeams.Rooms
{
    public class WebexRoomHandler : IRoomHandler
    {
        public Task<Room> GetRoomAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
