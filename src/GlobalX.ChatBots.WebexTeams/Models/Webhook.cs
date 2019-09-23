using System;

namespace GlobalX.ChatBots.WebexTeams.Models
{
    internal class Webhook
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string TargetUrl { get; set; }
        public string Resource { get; set; }
        public string Event { get; set; }
        public string Filter { get; set; }
        public string OrgId { get; set; }
        public string CreatedBy { get; set; }
        public string AppId { get; set; }
        public string OwnedBy { get; set; }
        public string Status { get; set; }
        public string Secret { get; set; }
        public DateTime Created { get; set; }
    }
}
