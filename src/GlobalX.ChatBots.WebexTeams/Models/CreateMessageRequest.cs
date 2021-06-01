namespace GlobalX.ChatBots.WebexTeams.Models {
    internal class CreateMessageRequest {
        public string RoomId { get; set; }
        public string ToPersonId { get; set; }
        public string ToPersonEmail { get; set; }
        public string Text { get; set; }
        public string Markdown { get; set; }
        public string ParentId { get; set; }
        public string[] Files { get; set; }
    }
}
