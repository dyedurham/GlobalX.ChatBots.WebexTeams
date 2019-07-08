using AutoMapper;
using GlobalX.ChatBots.WebexTeams.Mappers.Profiles;

namespace GlobalX.ChatBots.WebexTeams.Mappers
{
    public static class WebexTeamsMapperFactory
    {
        public static IMapper CreateMapper() {
            var config = new MapperConfiguration(c => {
                c.AddProfile<CommonMappers>();
                c.AddProfile<MessageMapper>();
            });

            config.AssertConfigurationIsValid();
            return config.CreateMapper();
        }
    }
}
