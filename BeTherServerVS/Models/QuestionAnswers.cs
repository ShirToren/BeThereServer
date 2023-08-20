using System;
namespace BeTherServer.Models
{
	public class QuestionAnswers
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string questionId { get; set; } = null!;
        public List<UserAnswer>? userAnswer { get; set; } = null!;
    }
}

