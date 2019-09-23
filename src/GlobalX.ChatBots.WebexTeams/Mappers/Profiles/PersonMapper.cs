using AutoMapper;
using GlobalXPerson = GlobalX.ChatBots.Core.People.Person;
using WebexTeamsPerson = GlobalX.ChatBots.WebexTeams.Models.Person;

namespace GlobalX.ChatBots.WebexTeams.Mappers.Profiles
{
    internal class PersonMapper : Profile
    {
        public PersonMapper() : this("PersonMapper") { }

        public PersonMapper(string profileName) : base(profileName)
        {
            CreateMap<WebexTeamsPerson, GlobalXPerson>()
                .ForMember(x => x.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Username, opt => opt.MapFrom(src => src.DisplayName));
        }
    }
}
