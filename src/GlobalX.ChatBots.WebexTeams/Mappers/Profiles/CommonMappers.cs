using AutoMapper;
using GlobalX.ChatBots.Core.Rooms;
using GlobalX.ChatBots.WebexTeams.Mappers.Converters;

namespace GlobalX.ChatBots.WebexTeams.Mappers.Profiles {
    internal class CommonMappers : Profile {
        public CommonMappers() : this("CommonMappers") {}

        public CommonMappers(string profileName) : base(profileName) {
            CreateMap<string, RoomType>()
                .ConvertUsing<StringToRoomTypeConverter>();
        }
    }
}
