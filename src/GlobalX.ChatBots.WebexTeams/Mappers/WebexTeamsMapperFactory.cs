using AutoMapper;
using GlobalX.ChatBots.WebexTeams.Mappers.Profiles;

namespace GlobalX.ChatBots.WebexTeams.Mappers
{
    internal static class WebexTeamsMapperFactory
    {
        public static IWebexTeamsMapper CreateMapper()
        {
            var config = new MapperConfiguration(c =>
            {
                c.AddProfile<CommonMappers>();
                c.AddProfile<MessageMapper>();
            });

            config.AssertConfigurationIsValid();
            var mapper = config.CreateMapper();
            return new WebexTeamsMapper(mapper);
        }
    }
}
