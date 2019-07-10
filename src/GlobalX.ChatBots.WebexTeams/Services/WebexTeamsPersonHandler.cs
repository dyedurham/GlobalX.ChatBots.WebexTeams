using System;
using System.Threading.Tasks;
using GlobalX.ChatBots.Core.People;

namespace GlobalX.ChatBots.WebexTeams.Services
{
    public class WebexTeamsPersonHandler : IPersonHandler
    {
        public Task<Person> GetPersonAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
