using System;

namespace GlobalX.ChatBots.WebexTeams.Models
{
    internal class MessageWebhookCallbackData
    {
        public string Id { get; set; }
        public string RoomId { get; set; }
        public string PersonId { get; set; }
        public string PersonEmail { get; set; }
        public DateTime Created { get; set; }
    }
}
