using System;

namespace GlobalX.ChatBots.WebexTeams.Models
{
    internal class Room
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public bool IsLocked { get; set; }
        public string TeamId { get; set; }
        public DateTime LastActivity { get; set; }
        public string CreatorId { get; set; }
        public DateTime Created { get; set; }
        public string SipAddress { get; set; }
    }
}
