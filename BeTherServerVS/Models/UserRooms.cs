namespace BeTherServer.Models
{
    public class UserRooms
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string username { get; set; } = null!;
        public HashSet<string> rooms { get; set; } = null!;
    }
}
