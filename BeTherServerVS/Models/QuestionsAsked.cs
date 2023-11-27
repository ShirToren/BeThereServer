using BeTherServer.Models;

namespace BeTherServer.Models
{
    public class QuestionAsked

    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = null!;
        public string chatRoomId { get; set; } = null!;
        
        public string question { get; set; } = null!;
        public string questionId { get; set; } = null!;
        public int? minimumAgeRange { get; set; } = null!;
        public int? maximumAgeRange { get; set; } = null!;
        public string gender { get; set; } = null!;
        public LocationData location { get; set; } = null!;
        public string date { get; set; } = null!;
        public string time { get; set; } = null!;
        public bool IsCityQuestion { get; set; } = false!;
        public string username { get; set; } = null!;
        public double radius { get; set; } = 0.0!;
        public int numOfAnswers { get; set; } = 0!;
    }
}