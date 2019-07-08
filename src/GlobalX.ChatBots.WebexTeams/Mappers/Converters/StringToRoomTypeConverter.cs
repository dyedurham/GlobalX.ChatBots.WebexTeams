using System;
using AutoMapper;
using GlobalX.ChatBots.Core.Rooms;

namespace GlobalX.ChatBots.WebexTeams.Mappers.Converters
{
    internal class StringToRoomTypeConverter : ITypeConverter<string, RoomType>
    {
        private const string Direct = "direct";
        private const string Group = "group";

        public RoomType Convert(string source, RoomType destination, ResolutionContext context)
        {
            switch(source)
            {
                case Direct:
                    return RoomType.Direct;
                case Group:
                    return RoomType.Group;
                default:
                    throw new ArgumentException($"Unknown enum value {source}", "source");
            }
        }
    }
}
