using System;
namespace Gamification.Models
{
	public class Answer
	{
		public int Id { get; set; }
		public Question Question { get; set; }
		public string AnswerType { get; set; }
		public string AnswerValue { get; set; }
		public bool CorrectAnswer { get; set; }
	}
}

