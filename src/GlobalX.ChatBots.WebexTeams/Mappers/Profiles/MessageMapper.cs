using AutoMapper;
using GlobalXMessage = GlobalX.ChatBots.Core.Messages.Message;
using WebexTeamsMessage = GlobalX.ChatBots.WebexTeams.Models.Message;

namespace GlobalX.ChatBots.WebexTeams.Mappers.Profiles
{
    public class MessageMapper : Profile
    {
        public MessageMapper() : this("MessageMapper") { }

        public MessageMapper(string profileName) : base(profileName)
        {
            CreateMap<WebexTeamsMessage, GlobalXMessage>()
                .ForMember(x => x.MessageParts, opt => opt.Ignore())
                .ForMember(x => x.SenderId, opt => opt.MapFrom(src => src.PersonId))
                .ForMember(x => x.SenderName, opt => opt.Ignore());
        }
    }
}
