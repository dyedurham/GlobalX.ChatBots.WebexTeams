using System;
using AutoMapper;
using GlobalX.ChatBots.Core.People;

namespace GlobalX.ChatBots.WebexTeams.Mappers.Converters
{
    internal class StringToPersonTypeConverter : ITypeConverter<string, PersonType>
    {
        private const string Person = "person";
        private const string Bot = "bot";

        public PersonType Convert(string source, PersonType destination, ResolutionContext context)
        {
            switch (source)
            {
                case Person:
                    return PersonType.Person;
                case Bot:
                    return PersonType.Bot;
                default:
                    throw new ArgumentException($"Unknown enum value {source}", nameof(source));
            }
        }
    }
}
