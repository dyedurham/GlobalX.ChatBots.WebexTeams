using System;
using AutoMapper;
using GlobalX.ChatBots.WebexTeams.Configuration;

namespace GlobalX.ChatBots.WebexTeams.Mappers.Converters
{
    internal class ResourceTypeToStringConverter : ITypeConverter<ResourceType, string>
    {
        private const string Memberships = "memberships";
        private const string Messages = "messages";
        private const string Rooms = "rooms";

        public string Convert(ResourceType source, string destination, ResolutionContext context)
        {
            switch (source)
            {
                case ResourceType.Memberships:
                    return Memberships;
                case ResourceType.Messages:
                    return Messages;
                case ResourceType.Rooms:
                    return Rooms;
                default:
                    throw new ArgumentException($"Unknown enum value {source}", nameof(source));
            }
        }
    }
}
