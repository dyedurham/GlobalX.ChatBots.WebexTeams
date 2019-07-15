using AutoMapper;
using GlobalXRoom = GlobalX.ChatBots.Core.Rooms.Room;
using WebexTeamsRoom = GlobalX.ChatBots.WebexTeams.Models.Room;

namespace GlobalX.ChatBots.WebexTeams.Mappers.Profiles
{
    internal class RoomMapper : Profile
    {
        public RoomMapper() : this("RoomMapper") { }

        public RoomMapper(string profileName) : base(profileName)
        {
            CreateMap<WebexTeamsRoom, GlobalXRoom>();
        }
    }
}
