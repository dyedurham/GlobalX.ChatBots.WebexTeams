using AutoMapper;
using GlobalX.ChatBots.WebexTeams.Configuration;
using GlobalX.ChatBots.WebexTeams.Mappers.Converters;
using GlobalX.ChatBots.WebexTeams.Models;

namespace GlobalX.ChatBots.WebexTeams.Mappers.Profiles
{
    internal class WebhookMapper : Profile
    {
        public WebhookMapper() : this("WebhookMapper") { }

        public WebhookMapper(string profileName) : base(profileName)
        {
            CreateMap<ResourceType, string>()
                .ConvertUsing<ResourceTypeToStringConverter>();
            CreateMap<EventType, string>()
                .ConvertUsing<EventTypeToStringConverter>();
            CreateMap<Configuration.Webhook, CreateWebhookRequest>()
                .ForMember(x => x.Secret, opt => opt.Ignore());
        }
    }
}
