namespace BeTherServer.Models
{
    public class UserData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string username { get; set; } = null!;
        public string password { get; set; } = null!;
        public int age { get; set; } = 0!;
        public string email { get; set; } = null!;
        public string gender { get; set; } = null!;
        public string fullName { get; set; } = null!;
        public int credits { get; set; } = 0!;

    }
}