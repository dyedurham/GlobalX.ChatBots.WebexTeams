using System;
using System.Threading.Tasks;
using GlobalX.ChatBots.Core.Rooms;
using GlobalX.ChatBots.WebexTeams.Mappers;

namespace GlobalX.ChatBots.WebexTeams.Services
{
    internal class WebexTeamsRoomHandler : IRoomHandler
    {
        private readonly IWebexTeamsApiService _apiService;
        private readonly IWebexTeamsMapper _mapper;

        public WebexTeamsRoomHandler(IWebexTeamsApiService apiService, IWebexTeamsMapper mapper)
        {
            _apiService = apiService;
            _mapper = mapper;
        }

        public async Task<Room> GetRoomAsync(string id)
        {
            var result = await _apiService.GetRoomAsync(id);
            var mappedResult = _mapper.Map<Room>(result);
            return mappedResult;
        }
    }
}
