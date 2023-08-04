namespace BeTherServer.Models
{
    public class UserData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string username { get; set; } = null!;
        public string name { get; set; } = null!;
        public string last_name { get; set; } = null!;
        public string email { get; set; } = null!;
        public string gender { get; set; } = null!;
        public string age { get; set; } = null!;
        public string password { get; set; } = null!;

    }
}