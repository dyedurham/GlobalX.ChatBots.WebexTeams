using System;
using AutoMapper;
using GlobalX.ChatBots.WebexTeams.Configuration;

namespace GlobalX.ChatBots.WebexTeams.Mappers.Converters
{
    internal class EventTypeToStringConverter : ITypeConverter<EventType, string>
    {
        private const string Created = "created";
        private const string Updated = "updated";
        private const string Deleted = "deleted";

        public string Convert(EventType source, string destination, ResolutionContext context)
        {
            switch (source)
            {
                case EventType.Created:
                    return Created;
                case EventType.Updated:
                    return Updated;
                case EventType.Deleted:
                    return Deleted;
                default:
                    throw new ArgumentException($"Unknown enum value {source}", nameof(source));
            }
        }
    }
}
