using AutoMapper;
using GlobalXMessage = GlobalX.ChatBots.Core.Messages.Message;
using WebexTeamsMessage = GlobalX.ChatBots.WebexTeams.Models.Message;

namespace GlobalX.ChatBots.WebexTeams.Mappers.Profiles
{
    internal class MessageMapper : Profile
    {
        public MessageMapper() : this("MessageMapper") { }

        public MessageMapper(string profileName) : base(profileName)
        {
            CreateMap<WebexTeamsMessage, GlobalXMessage>()
                .ForMember(x => x.MessageParts, opt => opt.Ignore())
                .ForMember(x => x.Sender, opt => opt.Ignore());
        }
    }
}
