using BeTherServer.Models;

namespace BeTherServer.Models
{
    public class QuestionAsked 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string question { get; set; } = null!;
        public int? minimumAgeRange { get; set; } = null!;
        public int? maximumAgeRange { get; set; } = null!;
        public string gender { get; set; } = null!;
        public double? locationLatitude { get; set; } = null!;
        public double? locationLongitude { get; set; } = null!;
        public string date { get; set; } = null!;
        public string time { get; set; } = null!;
        public string username { get; set; } = null!;
        public double? radius { get; set; } =null!;
    }
}