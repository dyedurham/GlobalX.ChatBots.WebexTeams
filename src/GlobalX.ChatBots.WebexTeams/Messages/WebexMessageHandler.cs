using System;
using System.Threading.Tasks;
using GlobalX.ChatBots.Core.Messages;

namespace GlobalX.ChatBots.WebexTeams.Messages
{
    public class WebexMessageHandler : IMessageHandler
    {
        public Task<Message> SendMessageAsync(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
