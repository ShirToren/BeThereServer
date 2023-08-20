namespace BeTherServer.Models
{
    public class UserAnswer
    {
        public string username { get; set; } = null!;
        public string text { get; set; } = null!;
        public string questionId { get; set; } = null!;
        public string chatRoomId { get; set; } = null!;
    }
}
