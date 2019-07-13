using System.Threading.Tasks;
using GlobalX.ChatBots.Core.People;
using GlobalX.ChatBots.WebexTeams.Mappers;

namespace GlobalX.ChatBots.WebexTeams.Services
{
    internal class WebexTeamsPersonHandler : IPersonHandler
    {
        private readonly IWebexTeamsApiService _apiService;
        private readonly IWebexTeamsMapper _mapper;

        public WebexTeamsPersonHandler(IWebexTeamsApiService apiService, IWebexTeamsMapper mapper)
        {
            _apiService = apiService;
            _mapper = mapper;
        }

        public async Task<Person> GetPersonAsync(string id)
        {
            var result = await _apiService.GetPersonAsync(id);
            var mappedResult = _mapper.Map<Person>(result);
            return mappedResult;
        }
    }
}
