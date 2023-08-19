namespace BeTherServer.Models
{
    public class Answer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string username { get; set; } = null!;
        public string text { get; set; } = null!;
        public string questionId { get; set; } = null!;
        public string chatRoomId { get; set; } = null!;
    }
}
